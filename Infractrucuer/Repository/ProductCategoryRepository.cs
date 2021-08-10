

using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using Appliction.Construct.ViewModel.ProductCategoryVM;
using Microsoft.EntityFrameworkCore;
using SMDomaim.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infractrucuer.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBaseClass<long,ProductCategory>,  IProductCategoryDomain
    {

        private readonly ShopContext _context;
        public ProductCategoryRepository(ShopContext context):base(context)
        {
            _context = context;
        }

       
        public EditProductcategoryViewModel GetDetails(long id)
        {
            var Item= _context.ProductCategories.Where(p=>p.Id==id)
                .Select(p=>new EditProductcategoryViewModel{

                    Id=id,
                    Description=p.Description,
                    Name=p.Name,
                    
                    Keywords=p.Keywords,
                    MetaDescription=p.MetaDescription,
                    PictureAlt=p.PictureAlt,
                    PictureTitle=p.PictureTitle,
                    Slug=p.Slug
                }).FirstOrDefault();
            return Item;
        }

        

        public List<ProductCategoryViewModel> Search(SearchViewModel searchViewModel)
        {
            var query=_context.ProductCategories.Select(p=>new ProductCategoryViewModel {
           
            CreationDate=p.Creation.ToFarsi(),
            Name=p.Name,
            Picture=p.Picture,
            Id=p.Id
            });

            if (!string.IsNullOrWhiteSpace(searchViewModel.Name))
                query=query.Where(x=> EF.Functions.Like(x.Name, $"%{x.Name}%"));
               // query=query.Where(p=>p.Name.Contains(searchViewModel.Name));

            return query.OrderByDescending(p => p.Id).ToList();

        }

        public List<ListProductCategoryViewModel> CategoryList()
        {
          return  _context.ProductCategories.Select(p => new ListProductCategoryViewModel
            {
              Id=p.Id,
              Name=p.Name
            }).ToList();
        }

        public string GetSlugCategoryById(long id)
        {
           return _context.ProductCategories.Select(c=>new {c.Id,c.Slug }).Where(c => c.Id == id).FirstOrDefault().Slug;
        }
    }
}
