using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.ProdcutPictureVM;
using Appliction.Construct.ViewModel.ProductCategoryVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Shop.Productcategory
{
    public class IndexModel : PageModel
    {
        public SearchViewModel SearchModel;
        public List<ProductCategoryViewModel> ProductCategories;

        private readonly IProductCategoryVM _productCategoryVM;
        public IndexModel(IProductCategoryVM productCategoryVM)
        {
            _productCategoryVM = productCategoryVM;
        }

        public void OnGet(SearchViewModel searchmodel)
        {
           ProductCategories=_productCategoryVM.SearchModel(searchmodel);

        }

        #region Create
        public IActionResult OnGetCreate()
        {
            return Partial("./Create",new CreateProductCategoryViewModel());
        }

        public JsonResult OnPostCreate(CreateProductCategoryViewModel command)
        {
          var result= _productCategoryVM.Creat(command);
            return new JsonResult(result);
           
        }
        #endregion
        #region Edit

        public IActionResult OnGetEdit(long id)
        {
            var productcategory = _productCategoryVM.GetDetial(id);
            return Partial("./Edit", productcategory);
        }

        public JsonResult OnPostEdit(EditProductcategoryViewModel command)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("مقدار صحیح نیست");
            }
            OperationResult result = _productCategoryVM.Edit(command);
            return new JsonResult(result);
        }

        #endregion
    }
}
