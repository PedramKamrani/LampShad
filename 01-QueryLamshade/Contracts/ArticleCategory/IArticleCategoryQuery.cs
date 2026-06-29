using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_QueryLamshade.Contracts.ArticleCategory
{
   public interface IArticleCategoryQuery
    {

        ArticleCategoryQueryModel GetArticleCategory(long id);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
