using _01_QueryLamshade.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent:ViewComponent
    {
        private readonly IProductCategory _productCategory;
        public ProductCategoryWithProductViewComponent(IProductCategory productCategory)
        {
            _productCategory = productCategory;
        }
        public IViewComponentResult Invoke()
        {
            var category = _productCategory.GetProductCategoriesWithProducts();
            return View(category);
        }
    }
}
