using _0_FrameWork.BaseClass;
using _01_QueryLamshade.Contracts.Product;
using _01_QueryLamshade.Contracts.ProductCategory;
using IM.InventoryEF;
using Infractracer.EF;
using Infractrucuer.EFCore;
using Microsoft.EntityFrameworkCore;
using SMDomaim.Model.ProuductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_QueryLamshade.ContractQurey
{
    public class ProductCategoryQuery : IProductCategory
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DisCountContext _discountContext;
        public ProductCategoryQuery(ShopContext context, InventoryContext InventoryContext, DisCountContext disCountContext)
        {
            _context = context;
            _inventoryContext = InventoryContext;
           _discountContext = disCountContext;
        }
        public List<ProductCategoryQueryModel> GetAllProductCategory()
        {
            return _context.ProductCategories.Select(c => new ProductCategoryQueryModel
            {
                Id = c.Id,
                Description = c.Description,
                Keywords = c.Keywords,
                MetaDescription = c.MetaDescription,
                Name = c.Name,
                Picture = c.Picture,
                PictureAlt = c.PictureAlt,
                PictureTitle = c.PictureTitle,
                Slug = c.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventories.Select(x =>
                new { x.ProductId, x.unitePrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var categories = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProduct(x.Products)
                }).AsNoTracking().ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var price = productInventory.unitePrice;
                        product.Price = price.ToMoney();
                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            int discountRate = discount.DiscountRate;
                            product.DiscountRate = discountRate;
                            product.HasDiscount = discountRate > 0;
                            var discountAmount = Math.Round((price * discountRate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }
                }
            }

            return categories;
        }

        private static List<ProductQueryModel> MapProduct(List<Product> products)
        {
          var productsMap=products.Select(c => new ProductQueryModel
            {
              Id=c.Id,
              Name=c.Name,
              Slug=c.Slug,
               Picture=c.Picture,
               PictureAlt=c.PictureAlt,
               PictureTitle=c.PictureTitle,
               Category=c.Category.Name
            });
            return productsMap.OrderByDescending(x=>x.Id).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoryBy(string id)
        {
            var discounts = _discountContext.CustomerDiscounts.
                Where(p => p.StartDate < DateTime.Now && p.EndDate > DateTime.Now)
                .Select(p => new { p.ProductId, p.DiscountRate,p.EndDate}).ToList();
            var inventores = _inventoryContext.Inventories.Select(p => new { p.ProductId, p.unitePrice }).ToList();

            var categoryProductBySlug = _context.ProductCategories.Include(p => p.Products)
                   .ThenInclude(p => p.Category).Select(p => new ProductCategoryQueryModel
                   {
                       Id=p.Id,
                       Slug=p.Slug,
                       Description=p.Description,
                       Keywords=p.Keywords,
                       MetaDescription=p.MetaDescription,
                       Name=p.Name,
                       Picture=p.Picture,
                       PictureAlt=p.PictureAlt,
                       PictureTitle=p.PictureTitle,
                       Products=MapProduct(p.Products)
                      
                   }).FirstOrDefault(p => p.Slug == id);

            foreach (var item in categoryProductBySlug.Products)
            {
                var inventory = inventores.FirstOrDefault(c => c.ProductId == item.Id);
                if (inventory != null)
                {
                    var discount = discounts.FirstOrDefault(c => c.ProductId == item.Id);
                    var price = inventory.unitePrice;
                    item.Price = price.ToMoney();
                    if (discount != null)
                    {
                        int discuontrate = discount.DiscountRate;
                        item.HasDiscount = discuontrate > 0;
                       var discountAmount=Math.Round((price*discuontrate)/100);
                        item.PriceWithDiscount = (price - discountAmount).ToMoney();
                        item.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    }
                }
            }
            return categoryProductBySlug;
        }
    }
}

