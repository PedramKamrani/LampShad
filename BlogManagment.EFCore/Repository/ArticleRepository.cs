using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using BlogManagmentApplication.Contract.ArticleVM;
using BlogManagmentDomain.ArticleDomainAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagment.EFCore.Repository
{
    public class ArticleRepository : RepositoryBaseClass<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;
        public ArticleRepository(BlogContext context):base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Where(z => z.Id == id).Select(z => new EditArticle
            {
                Id=z.Id,
                Description=z.Description,
                Keywords=z.Keywords,
               PictureAlt=z.PictureAlt,
               PictureTitle=z.PictureTitle,
               PublishDate=z.PublishDate.ToFarsi(),
               ShortDescription=z.ShortDescription,
               Slug=z.Slug,
               Title=z.Title,
               CategoryId=z.CategoryId,
               CanonicalAddress=z.CanonicalAddress,
               MetaDescription=z.MetaDescription,
             
            }).FirstOrDefault();
        }

        public Article GetWithCategory(long id)
        {
            return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Select(x => new ArticleViewModel
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Category = x.Category.Name,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.ShortDescription.Length, 50)) + " ...",
                Title = x.Title
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => EF.Functions.Like(x.Title,$"%{searchModel.Title}%"));
                //query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
