using _01_QueryLamshade.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent:ViewComponent
    {
        private readonly IProductCategory _productCategory;
        public ProductCategoryViewComponent(IProductCategory productCategory)
        {
            _productCategory = productCategory;
        }
        public IViewComponentResult Invoke()
        {
            var productCategory = _productCategory.GetAllProductCategory();
            return View(productCategory);
        }
    }
}
