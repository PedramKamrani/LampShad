using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.ProdcutPictureVM;
using SMDomaim.Model;
using SMDomaim.Model.ProductPictureAgg;
using SMDomaim.Model.ProuductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.ProductPictureApp
{
    public class ProductPictureReopsitoryApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepositoryDomain _repositoryDomain;
        private readonly IProductCategoryDomain _repositoryProductCategoryDomain;
        private readonly IFileUploader _fileUploader;
        public ProductPictureReopsitoryApplication(IProductCategoryDomain repositoryProductCategoryDomain, IProductPictureRepositoryDomain repositoryDomain, IFileUploader fileUploader)
        {
            _repositoryDomain = repositoryDomain;
            _repositoryProductCategoryDomain = repositoryProductCategoryDomain;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateProductPicture command)
        {
            string path = "";
          var operation = new OperationResult();
            var product = _repositoryDomain.GetWithProductAndCategory(command.ProductId);
            if (product != null)
            {
                 path = $"{product.Product.Category}//{product.Product.Slug}";
            }
            path = $"{command.PictureTitle}";
            var pictuerM =_fileUploader.Upload(command.Picture,path);
            var productpictuer = new ProductPicture(command.ProductId, pictuerM, command.PictureAlt, command.PictureTitle);
            _repositoryDomain.Create(productpictuer);
            _repositoryDomain.SaveChanges();
            return operation.Succedded();
            
           
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _repositoryDomain.GetWithProductAndCategory(command.Id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            
            var path = $"{productPicture.Product.Category.Slug}//{productPicture.Product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            productPicture.Edit(command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);
            _repositoryDomain.SaveChanges();
            return operation.Succedded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _repositoryDomain.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var oprtion = new OperationResult();
            ProductPicture productPicture = _repositoryDomain.Get(id);
            if (productPicture == null)
                return oprtion.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            _repositoryDomain.SaveChanges();
            return oprtion.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var oprtion = new OperationResult();
            ProductPicture productPicture = _repositoryDomain.Get(id);
            if (productPicture == null)
                return oprtion.Failed(ApplicationMessages.RecordNotFound);
           
                productPicture.Restore();
                _repositoryDomain.SaveChanges();
                return oprtion.Succedded();
            
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _repositoryDomain.Search(searchModel);
        }
    }
}
