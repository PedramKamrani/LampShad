using _0_Framework.Application;
using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.ProductVM;
using SMDomaim.Model;
using SMDomaim.Model.ProuductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.ProductApp
{
    public class ProductApplication : IProductAppliction
    {
        private readonly IProductRepositoryDoamin _productRepositoryDoamin;
        private readonly IProductCategoryDomain _productcategoryrepository;
        private readonly IFileUploader _fileUploader;
        public ProductApplication(IProductRepositoryDoamin productRepositoryDoamin, IFileUploader fileUploader,
            IProductCategoryDomain productcategoryrepository)
        {
            _productRepositoryDoamin = productRepositoryDoamin;
            _productcategoryrepository = productcategoryrepository;
            _fileUploader = fileUploader;
        }
        public OperationResult Creat(CreateProductViewModel command)
        {

            OperationResult optionResult = new OperationResult();
            if (_productRepositoryDoamin.Exists(x => x.Name == command.Name))
                return optionResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var slugcategory = _productcategoryrepository.GetSlugCategoryById(command.CategoryId);
            var FileName =_fileUploader.Upload(command.Picture,slugcategory);
            var product = new Product(command.Name,command.Code,
                command.ShortDescription,command.Description,FileName
                ,command.PictureAlt,command.PictureTitle,
                command.CategoryId,slug,command.Keywords,command.MetaDescription);
            _productRepositoryDoamin.Create(product);
            _productRepositoryDoamin.SaveChanges();
           return optionResult.Succedded();
        }

        public OperationResult Edit(EditProductViewModel command)
        {
            OperationResult optionResult = new OperationResult();
            var product = _productRepositoryDoamin.Get(command.Id);
            if (product == null)
                return optionResult.Failed(ApplicationMessages.RecordNotFound);

            //if (_productRepositoryDoamin.Exsist(x => x.Name == command.Name&&x.Id!=command.Id))
            //    return optionResult.Faild(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var slugcategory = _productcategoryrepository.GetSlugCategoryById(command.CategoryId);
            var FileName = _fileUploader.Upload(command.Picture, slugcategory);
            product.Edit(command.Name, command.Code, command.ShortDescription, command.Description,
                FileName, command.PictureAlt, command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);
            _productRepositoryDoamin.SaveChanges();
            return optionResult.Succedded();
        }

        public List<ProductViewModel> GetAllProduct()
        {
          return _productRepositoryDoamin.GetAllProducts();
        }

        public EditProductViewModel GetDetails(long id)
        {
            return _productRepositoryDoamin.GetDetail(id);
        }

        public OperationResult Isstack(long Id)
        {
            OperationResult optionResult = new OperationResult();
            Product product = _productRepositoryDoamin.Get(Id);
            if (product == null)
                return optionResult.Failed(ApplicationMessages.RecordNotFound);
            product.IsStacked();
            _productRepositoryDoamin.SaveChanges();
            return optionResult.Succedded();
                
        }

        public OperationResult NotIsstack(long Id)
        {
            OperationResult optionResult = new OperationResult();
            Product product = _productRepositoryDoamin.Get(Id);
            if (product == null)
                return optionResult.Failed(ApplicationMessages.RecordNotFound);
            product.IsNoStacked();
            _productRepositoryDoamin.SaveChanges();
            return optionResult.Succedded();
        }

        public List<ProductViewModel> SearchProduct(ProductSearchModelViewModel searchmodel)
        {
            return _productRepositoryDoamin.SearchProduct(searchmodel);
        }
    }
}
