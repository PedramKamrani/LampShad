using BlogManagment.EFCore.Mapping;
using BlogManagmentDomain.ArticleCategoryDomain;
using BlogManagmentDomain.ArticleDomainAgg;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlogManagment.EFCore
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options):base(options)
        {
            
        }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Assmbely = typeof(ArticleCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(Assmbely);

        }
    }
}
