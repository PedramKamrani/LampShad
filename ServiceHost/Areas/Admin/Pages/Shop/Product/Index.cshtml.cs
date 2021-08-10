using _0_FrameWork.RepositoryBase;
using Appliction.Construct.ViewModel.ProductCategoryVM;
using Appliction.Construct.ViewModel.ProductVM;
using ConfigurationLayer.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Admin.Pages.Shop.Product
{
    [Authorize(Roles =Roles.Administrator)]
    public class IndexModel : PageModel
    {
        private readonly IProductAppliction _productAppliction;
        private readonly IProductCategoryVM _productCategoryVM;

        public List<ProductViewModel> productViewModels;
        public ProductSearchModelViewModel SearchModel;
        public SelectList  ProductCategories;
        

        public IndexModel(IProductAppliction productAppliction, IProductCategoryVM productCategoryVM)
        {
            _productAppliction = productAppliction;
            _productCategoryVM = productCategoryVM;
        }
        public void OnGet(ProductSearchModelViewModel searchModel)
        {
            ProductCategories =new SelectList(_productCategoryVM.CategoryList(),"Id","Name");
          productViewModels=  _productAppliction.SearchProduct(searchModel);
        }

        #region Create
        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateProductViewModel
            {
                Categories = _productCategoryVM.CategoryList()
            };
            return Partial("./Create",command);
        }
        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult OnPostCreate(CreateProductViewModel command)
        {
           var result= _productAppliction.Creat(command);
            return new JsonResult(result);
        }
        #endregion


        #region Edit
        public IActionResult OnGetEdit(long id)
        {
            var product = _productAppliction.GetDetails(id);
            product.Categories = _productCategoryVM.CategoryList();
            return Partial("./Edit",product);
        }
        public IActionResult OnPostEdit(EditProductViewModel command)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("مقدار صحیح نیست");
            }
            var result = _productAppliction.Edit(command);
            return new JsonResult(result);
        }
        #endregion
    }
}
