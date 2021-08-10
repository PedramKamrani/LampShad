using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contract.ViewModel.CustomerDiscountVM;
using Appliction.Construct.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Admin.Pages.DisCounts.CustomerDisCount
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Products;
        private readonly ICustomerDiscountApplication _discountApplication;
        private readonly IProductAppliction _productAppliction;
        public IndexModel(ICustomerDiscountApplication discountApplication, IProductAppliction productAppliction)
        {
            _discountApplication = discountApplication;
            _productAppliction = productAppliction;
        }
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
           
            Products = new SelectList(_productAppliction.GetAllProduct(), "Id", "Name");
            CustomerDiscounts = _discountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productAppliction.GetAllProduct()
            };

            return Partial("./Create",command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _discountApplication.Define(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetEdit(long id)
        {
            var command = _discountApplication.GetDetails(id);
            command.Products = _productAppliction.GetAllProduct();

            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditCustoemrDiscount command)
        {
            var result = _discountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
