using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_QueryLamshade.Contracts.ProductCategory
{
   public interface IProductCategory
    {
        List<ProductCategoryQueryModel> GetAllProductCategory();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
        ProductCategoryQueryModel GetProductCategoryBy(string id);
    }
}
