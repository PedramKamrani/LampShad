using _01_QueryLamshade.Contracts.ArticleCategory;
using _01_QueryLamshade.Contracts.ProductCategory;
using System.Collections.Generic;

namespace _01_QueryLamshade.Contracts
{
   public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
