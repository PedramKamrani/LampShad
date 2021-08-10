using _0_FrameWork.RepositoryBase;
using _01_QueryLamshade.ContractQurey;
using _01_QueryLamshade.Contracts.Product;
using _01_QueryLamshade.Contracts.ProductCategory;
using _01_QueryLamshade.Contracts.Slide;
using ApplicationLayer;
using ApplicationLayer.BannerApp;
using ApplicationLayer.OrderApp;
using ApplicationLayer.ProductApp;
using ApplicationLayer.ProductPictureApp;
using ApplicationLayer.SlideApp;
using Appliction.Construct.ViewModel.BannerVM;
using Appliction.Construct.ViewModel.Order;
using Appliction.Construct.ViewModel.ProdcutPictureVM;
using Appliction.Construct.ViewModel.ProductCategoryVM;
using Appliction.Construct.ViewModel.ProductVM;
using Appliction.Construct.ViewModel.SlideViewModel;
using ConfigurationLayer.Permissions;
using Infractrucuer.EFCore;
using Infractrucuer.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Infrastructure.InventoryAcl;
using SMDomaim.Model;
using SMDomaim.Model.BannerAgg;
using SMDomaim.Model.OrderAgg;
using SMDomaim.Model.ProductPictureAgg;
using SMDomaim.Model.ProuductAgg;
using SMDomaim.Model.Service;
using SMDomaim.Model.SlideAgg;

namespace ConfigurationLayer
{
    public  class ShopManagmentBootstraper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IProductCategoryDomain, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryVM, ProductCategoryAppliction>();

            services.AddTransient<IProductRepositoryDoamin,ProductRepository>();
            services.AddTransient<IProductAppliction,ProductApplication>();

            services.AddTransient<IProductPictureApplication, ProductPictureReopsitoryApplication>();
            services.AddTransient<IProductPictureRepositoryDomain,ProductPictureRepository>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();

            services.AddTransient<ISlideDomain,SlideRepository>();
            services.AddTransient<ISlideApplication,SlideApplication>();


            services.AddTransient<IBannerRepository,BannerRepository>();
            services.AddTransient<IBannerApplicationcs, BannerApplication>();

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategory, ProductCategoryQuery>();

            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IPermissonExposer, ShopPermissionExposer>();

            services.AddTransient<ICartCalculatorService, CartCalculatorService>();

            services.AddTransient<IShopInventoryAcl, ShopInventoryAcl> ();

            services.AddSingleton<ICartService, CartService>();
          
            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
