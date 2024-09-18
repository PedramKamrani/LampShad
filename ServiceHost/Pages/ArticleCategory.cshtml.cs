using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_QueryLamshade.Contracts.Article;
using _01_QueryLamshade.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
       public ArticleCategoryQueryModel ArticleCategory;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public List<ArticleQueryModel> LatestArticles;

        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string id)
        {
            if (id == "manifest.json") return;
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategory = _articleCategoryQuery.GetArticleCategory(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();

        }
    }
}
