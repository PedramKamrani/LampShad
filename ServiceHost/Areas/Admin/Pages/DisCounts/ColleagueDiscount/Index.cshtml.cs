using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contract.ViewModel.ColleagueDiscountVM;
using Appliction.Construct.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Admin.Pages.DisCounts.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
       public List<ColleagueDiscounViewModel> Colleagues;
       public SelectList Products;
       public ColleagueSearchModel SearchModel;

        private readonly IColleagueApplication _application;
        private readonly IProductAppliction _productApp;
        public IndexModel(IColleagueApplication application, IProductAppliction productApp)
        {
            _productApp = productApp;
            _application = application;
        }
        public void OnGet(ColleagueSearchModel searchModel)
        {
            Products =new SelectList(_productApp.GetAllProduct(),"Id","Name");
            Colleagues = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApp.GetAllProduct()
            };
        
            return Partial("./Create",command);
        }
        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _application.Define(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
           
            var discount = _application.GetDetails(id);
            discount.Products = _productApp.GetAllProduct();
            return Partial("./Edit", discount);
        }
        public JsonResult OnPostEdit(EditcolleagueDiscountViewModel command)
        {
            var result = _application.Edit(command);

            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            var item = _application.Remove(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            var item = _application.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
