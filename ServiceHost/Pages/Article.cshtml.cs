using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_FrameWork.BaseClass;
using _01_QueryLamshade.Contracts.Article;
using _01_QueryLamshade.Contracts.ArticleCategory;
using CommenetManagmenrt.Infractracer.EFCore;
using Comment.Managment.Cantract.Comment;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery ,ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
           HtmlSanitizer htmlsantaizer = new HtmlSanitizer();
            AddComment santazier = new AddComment
            {
                Email=htmlsantaizer.Sanitize(command.Email),
                Message=htmlsantaizer.Sanitize(command.Message),
                Name=htmlsantaizer.Sanitize(command.Name),
                Website=htmlsantaizer.Sanitize(command.Website)
            };
            command.Type = CommentType.Article;
            OperationResult result = _commentApplication.Add(santazier);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
