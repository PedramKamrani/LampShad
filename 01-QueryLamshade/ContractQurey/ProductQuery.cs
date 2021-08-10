using _0_FrameWork.BaseClass;
using _01_QueryLamshade.Contracts.Product;
using Appliction.Construct.ViewModel.Order;
using CommenetManagmenrt.Infractracer.EFCore;
using IM.InventoryEF;
using Infractracer.EF;
using Infractrucuer.EFCore;
using Microsoft.EntityFrameworkCore;
using SMDomaim.Model.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;


namespace _01_QueryLamshade.ContractQurey
{
   public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopcontext;
        private readonly InventoryContext _Inventorycontext;
        private readonly DisCountContext _discountcontext;
        private readonly CommentContext _commentcontext;
        public ProductQuery(ShopContext shopcontext, InventoryContext Inventorycontext,
            DisCountContext discountcontext, CommentContext commentcontext)
        {
            _shopcontext = shopcontext;
            _discountcontext = discountcontext;
            _Inventorycontext= Inventorycontext;
            _commentcontext = commentcontext;
        }

        public List<ProductQueryModel> GetProductArrivals()
        {
            var inventores = _Inventorycontext.Inventories.Select(i => new {i.ProductId ,i.unitePrice}).ToList();
            var discunts = _discountcontext.CustomerDiscounts.Where(c=>c.StartDate<DateTime.Now&&c.EndDate>DateTime.Now)
                .Select(c => new { c.ProductId, c.DiscountRate,c.EndDate }).ToList();
            var productss = _shopcontext.Products.Include(p=>p.Category).Select(p => new ProductQueryModel
            {
                Id=p.Id,
                Name=p.Name,
                Picture=p.Picture,
                Slug=p.Slug,
                PictureTitle=p.PictureTitle,
                PictureAlt=p.PictureAlt,
                Category=p.Category.Name
            }).AsNoTracking().OrderByDescending(p=>p.Id).Take(5).ToList();
            foreach (var Product in productss)
            {
                var inventory = inventores.FirstOrDefault(p => p.ProductId == Product.Id);
                if (inventory != null)
                {
                    var price = inventory.unitePrice;
                    Product.Price = price.ToMoney();
                    var discount = discunts.FirstOrDefault(p => p.ProductId == Product.Id);
                    if (discount != null)
                    {
                        int discountrate = discount.DiscountRate;
                        Product.DiscountRate = discountrate;
                        Product.HasDiscount = discountrate > 0;
                        Product.DiscountExpireDate=discount.EndDate.ToDiscountFormat();
                        var DiscountAmount = Math.Round((price*discountrate)/100);
                         Product.PriceWithDiscount = (price - DiscountAmount).ToMoney();
                    }

                }
            }
            return productss;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventores = _Inventorycontext.Inventories.Select(x => new { x.ProductId, x.unitePrice }).ToList();
            var discounts = _discountcontext.CustomerDiscounts.Where(x=>x.StartDate<DateTime.Now&&x.EndDate>DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate,x.EndDate }).ToList();

            var Query = _shopcontext.Products.Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                   Id=x.Id,
                   CategorySlug=x.Category.Slug,
                   Slug=x.Slug,
                   Description=x.Description,
                   Name=x.Name,
                   ShortDescription=x.ShortDescription,
                   Picture=x.Picture,
                   PictureTitle=x.PictureTitle,
                   PictureAlt=x.PictureAlt,
                  
                }).AsNoTracking();
            if (!String.IsNullOrWhiteSpace(value))
                Query = Query.Where(x => EF.Functions.Like(x.Name,$"%{value}%")||EF.Functions.Like(x.ShortDescription,$"%{value}%"));
               // Query = Query.Where(x => x.Name.Contains(x.Name)||x.ShortDescription.Contains(x.ShortDescription));

            var Products = Query;
            foreach (var Product in Products)
            {
                var ProductInventory = inventores.FirstOrDefault(x => x.ProductId == Product.Id);
                if (ProductInventory != null)
                {
                    var Price = ProductInventory.unitePrice;
                    Product.Picture = Price.ToMoney();
                    var discount = discounts.FirstOrDefault(x => x.ProductId == Product.Id);
                    if (discount != null)
                    {
                        int discountRate = discount.DiscountRate;
                        Product.DiscountRate = discountRate;
                        Product.HasDiscount = discountRate > 0;
                        Product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                        var PriceAmount = Math.Round((Price * discountRate) / 100);
                        Product.PriceWithDiscount = (Price - PriceAmount).ToMoney();
                    }

                }
            }
           return Products.OrderByDescending(x => x.Id).ToList();
        }


        public ProductQueryModel GetProductDetails(string slug)
        {
            var inventory = _Inventorycontext.Inventories.Select(x => new { x.ProductId, x.unitePrice, x.InStock }).ToList();
            var comments = _commentcontext.Comments.Select(x => new {x.Id,x.Message}).ToList();
            var discounts = _discountcontext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var product = _shopcontext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    Code = x.Code,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    Pictures = MapProductPictures(x.ProductPictures)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return new ProductQueryModel();

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                product.IsInStock = productInventory.InStock;
                var price = productInventory.unitePrice;
                product.Price = price.ToMoney();
                product.DoublePrice = price;
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            product.Comments =_commentcontext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Product)
                .Where(x => x.OwnerRecordId == product.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    CreationDate = x.Creation.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            return product;
        }

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> pictures)
        {
            return pictures.Select(x => new ProductPictureQueryModel
            {
                IsRemoved = x.IsRemoved,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).Where(x => !x.IsRemoved).ToList();
        }
        public List<Cartitem> CheckInventoryStatus(List<Cartitem> cartItems)
        {
            var inventory = _Inventorycontext.Inventories.ToList();

            foreach (var cartItem in cartItems.Where(cartItem =>
                inventory.Any(x => x.ProductId == cartItem.Id && x.InStock)))
            {
                var itemInventory = inventory.Find(x => x.ProductId == cartItem.Id);
                cartItem.IsInStock = itemInventory.CalcualateCurrentInventoryStock() >= cartItem.Count;
            }

            return cartItems;
        }
    }
}
