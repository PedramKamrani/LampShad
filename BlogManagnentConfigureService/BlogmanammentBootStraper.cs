using _01_QueryLamshade.ContractQurey;
using _01_QueryLamshade.Contracts.Article;
using _01_QueryLamshade.Contracts.ArticleCategory;
using BlogManagment.EFCore;
using BlogManagment.EFCore.Repository;
using BlogManagmentApplication;
using BlogManagmentApplication.Contract.ArticleCategoryVM;
using BlogManagmentApplication.Contract.ArticleVM;
using BlogManagmentDomain.ArticleCategoryDomain;
using BlogManagmentDomain.ArticleDomainAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlogManagnentConfigureService
{
    public  class BlogmanammentBootStraper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IAricleCategoryRepository, ArticleCategoryRepository>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository, ArticleRepository>();

            services.AddTransient<IArticleQuery, ArticleQuery>();
            services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();

            services.AddDbContext<BlogContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
