using System;
using System.Collections.Generic;
using System.Linq;
using _01_QueryLamshade.Contracts.Product;
using Appliction.Construct.ViewModel.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<Cartitem> CartItem;
        public const string CookieName = "cart-items";
        private readonly IProductQuery _productQuery;
        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
            CartItem = new List<Cartitem>();
        }
        public void OnGet()
        {
            JavaScriptSerializer Serilaizer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
          var CartItems= Serilaizer.Deserialize<List<Cartitem>>(value);
            if (CartItems == null)
            {
                CartItem = new List<Cartitem>();
            }
            else
            {

                foreach (var item in CartItems)
                    item.CalculateTotalItemPrice();

                CartItem = _productQuery.CheckInventoryStatus(CartItems);
            }
           

        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            JavaScriptSerializer Serilaizer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
          List<Cartitem> CartItems = Serilaizer.Deserialize<List<Cartitem>>(value);
            var itemToRemove = CartItems.FirstOrDefault(x => x.Id == id);
            CartItems.Remove(itemToRemove);
            var option = new CookieOptions {Expires = DateTime.Now.AddDays(2)};
            Response.Cookies.Append(CookieName, Serilaizer.Serialize(CartItems), option);
            return RedirectToPage("/Cart");
        }
        public IActionResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<Cartitem>>(value);
            foreach (var item in cartItems)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;
            }

            CartItem = _productQuery.CheckInventoryStatus(cartItems);

           

            return RedirectToPage(CartItem.Any(x => !x.IsInStock) ? "/Cart" : "/Checkout");
        }
    }
}
