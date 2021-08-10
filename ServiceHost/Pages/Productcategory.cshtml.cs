using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_QueryLamshade.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductcategoryModel : PageModel
    {
        public ProductCategoryQueryModel ProductCategory;
        private readonly IProductCategory _productCategory;

        public ProductcategoryModel(IProductCategory productCategory)
        {
            _productCategory = productCategory;
        }

        public void OnGet(string id)
        {
            ProductCategory = _productCategory.GetProductCategoryBy(id);
            
        }

        
    }
}
