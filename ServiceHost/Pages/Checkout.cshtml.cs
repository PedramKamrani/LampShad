using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using _0_FrameWork.BaseClass;
using _0_FrameWork.BaseClass.Sms;
using _0_FrameWork.BaseClass.ZarinPal;
using _01_QueryLamshade.ContractQurey;
using _01_QueryLamshade.Contracts.Product;
using Appliction.Construct.ViewModel.Order;
using InventoryApplicationContract.InventoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ZarinpalSandbox;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ISmsService _smsService;
        private readonly IInventoryApplication _inventory;

        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService,
            IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory,
            IAuthHelper authHelper, ISmsService smsService, IInventoryApplication inventory)
        {
            Cart = new Cart();
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
            _smsService = smsService;
            _inventory = inventory;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<Cartitem>>(value);
            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            Cart = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(Cart);
        }

        public IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);

            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");
            string mobile = User.FindFirst("Mobile").Value;
            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod == 1)
            {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                    cart.PayAmount.ToString(CultureInfo.InvariantCulture), mobile, "",
                    "خرید از درگاه لوازم خانگی و دکوری", orderId);

                return Redirect(
                    $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }

            var paymentResult = new PaymentResult();
            _smsService.Send("09186622720", $"درتاریخ{DateTime.Now.ToFarsi()}یک خریدبا کدسفارش در محل ثبت شد");
            return RedirectToPage("/PaymentResult",
                paymentResult.Succeeded(
                    "سفارش شما با موفقیت ثبت شد. پس از تماس کارشناسان ما و پرداخت وجه، سفارش ارسال خواهد شد.", null));
        }

        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {

            ///reducer inventory

            var cart = _cartService.Get();
            ReduceInventory commInventory = new ReduceInventory();

            List<ReduceInventory> listinventory = new List<ReduceInventory>();
            List<ReduceInventory> endInventories = new List<ReduceInventory>();

            foreach (var cartitem in cart.Items)
            {
                List<ReduceInventory> re = new List<ReduceInventory>
              {
                  new  ReduceInventory(){Count = cartitem.Count,
                      Description = "فروش آنلاین",
                      ProductId = cartitem.Id,
                      OrderId = oId}
              };
                listinventory.AddRange(re);
            }
            ///end inventory reduce
            var orderAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse =
                _zarinPalFactory.CreateVerificationRequest(authority,
                    orderAmount.ToString(CultureInfo.InvariantCulture));
            string mobile = User.FindFirst("Mobile").Value;
            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status == 100)
            {
                var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
                _inventory.Reduce(listinventory);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded("پرداخت با موفقیت انجام شد.", issueTrackingNo);
                _smsService.Send("09186622720", $"درتاریخ{DateTime.Now.ToFarsi()}یک خریدبا کد{verificationResponse.RefID}ثبت شد");
                _smsService.Send(mobile, $"شماره پیگیری شما{issueTrackingNo}");
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed(
                "پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");
            return RedirectToPage("/PaymentResult", result);
        }
    }
}
