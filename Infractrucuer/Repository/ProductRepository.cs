using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using Appliction.Construct.ViewModel.ProductVM;
using SMDomaim.Model.ProuductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infractrucuer.EFCore.Repository
{
    public class ProductRepository : RepositoryBaseClass<long, Product>, IProductRepositoryDoamin
    {
        private ShopContext _context;
        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductViewModel> GetAllProducts()
        {
           return _context.Products.Select(p=>new ProductViewModel 
           {
               Id=p.Id,
               Category=p.Category.Name,
               Name=p.Name,
               CategoryId=p.CategoryId,
               Code=p.Code,
               CreationDate=p.Creation.ToFarsi(),
               Picture=p.Picture
           
           }).ToList();
        }

        public EditProductViewModel GetDetail(long id)
        {
           return _context.Products.Select(p=>new EditProductViewModel
           {
               Id=p.Id,
               Code=p.Code,
               CategoryId=p.CategoryId,
               Description=p.Description,
               Keywords=p.Keywords,
               Name=p.Name,
              
               PictureAlt=p.PictureAlt,
               PictureTitle=p.PictureTitle,
               MetaDescription=p.MetaDescription,
               ShortDescription=p.ShortDescription,
               Slug=p.Slug
           }).FirstOrDefault(p=>p.Id==id);
        }

        public List<ProductViewModel> SearchProduct(ProductSearchModelViewModel searchmodel)
        {
            var query = _context.Products.Select(s=>new ProductViewModel
            { 
             
            Id=s.Id,
            CategoryId=s.CategoryId,
            Code=s.Code,
            CreationDate=s.Creation.ToFarsi(),
            Name=s.Name,
            Picture=s.Picture,
            Category=s.Category.Name
            });
            if (searchmodel.Name != null)
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{x.Name}%"));
                //query = query.Where(s => s.Name.Contains(searchmodel.Name));

            if (searchmodel.Code != null)
                query = query.Where(x => EF.Functions.Like(x.Code, $"%{x.Code}%"));
                //query = query.Where(s => s.Code.Contains(searchmodel.Code));
            if (searchmodel.CategoryId != 0)
                query = query.Where(s => s.CategoryId==searchmodel.CategoryId);

            return query.OrderBy(p => p.Id).ToList();
        }


       
    }
}
