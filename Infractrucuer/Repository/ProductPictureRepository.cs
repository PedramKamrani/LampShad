using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using Appliction.Construct.ViewModel.ProdcutPictureVM;
using Microsoft.EntityFrameworkCore;
using SMDomaim.Model.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace Infractrucuer.EFCore.Repository
{
   public class ProductPictureRepository:RepositoryBaseClass<long, ProductPicture>,IProductPictureRepositoryDomain
    {
        private readonly ShopContext _context;
        public ProductPictureRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures
                .Select(x => new EditProductPicture
                {
                    Id = x.Id,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductId = x.ProductId
                }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures
                .Include(x => x.Product)
                .Select(x => new ProductPictureViewModel
                {
                    Id = x.Id,
                    Product = x.Product.Name,
                    CreationDate = x.Creation.ToFarsi(),
                    Picture = x.Picture,
                    ProductId = x.ProductId,
                    IsRemoved = x.IsRemoved
                });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }


        public ProductPicture GetWithProductAndCategory(long id)
        {
           var category= _context.ProductPictures
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == id);

            return category;
        }
    }
}
