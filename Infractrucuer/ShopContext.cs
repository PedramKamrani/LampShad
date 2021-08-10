using Microsoft.EntityFrameworkCore;
using SMDomaim.Model;
using SMDomaim.Model.BannerAgg;
using SMDomaim.Model.OrderAgg;
using SMDomaim.Model.ProductPictureAgg;
using SMDomaim.Model.ProuductAgg;
using SMDomaim.Model.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractrucuer.EFCore
{
   public class ShopContext:DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assmbely = typeof(ProductCategory).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assmbely);
        }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
