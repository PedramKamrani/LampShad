using _0_Framework.Application;
using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.ProductCategoryVM;
using SMDomaim.Model;
using System;
using System.Collections.Generic;

namespace ApplicationLayer
{
    public class ProductCategoryAppliction : IProductCategoryVM
    {
        private readonly IProductCategoryDomain _repositoryDoamin;
        private readonly IFileUploader _fileUploader;
        public ProductCategoryAppliction(IProductCategoryDomain repositoryDoamin, IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
            _repositoryDoamin = repositoryDoamin;
        }

        public List<ListProductCategoryViewModel> CategoryList()
        {
            return _repositoryDoamin.CategoryList();
        }

        public OperationResult Creat(CreateProductCategoryViewModel command)
        {
            var slug = command.Slug.Slugify();
            var pictuerpath = $"{command.Slug}";
            var FileName = _fileUploader.Upload(command.Picture, pictuerpath);
            OperationResult optionResult = new OperationResult();
            if (_repositoryDoamin.Exists(e => e.Name == command.Name))
                return optionResult.Failed(ApplicationMessages.DuplicatedRecord);



            ProductCategory productCategory = new ProductCategory(command.Name, command.Description, FileName,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _repositoryDoamin.Create(productCategory);
            _repositoryDoamin.SaveChanges();
            return optionResult.Succedded();
        }

        public OperationResult Edit(EditProductcategoryViewModel command)
        {

            var operation = new OperationResult();
            var productCategory = _repositoryDoamin.Get(command.Id);

            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            //if (_repositoryDoamin.Exsist(x => x.Name == command.Name && x.Id != command.Id))
            //    return operation.Faild(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();

            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            productCategory.Edit(command.Name, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);

            _repositoryDoamin.SaveChanges();
            return operation.Succedded();
        }

        public EditProductcategoryViewModel GetDetial(long Id)
        {
            return _repositoryDoamin.GetDetails(Id);
        }

        public string GetSlugCategoryById(long id)
        {
            return _repositoryDoamin.GetSlugCategoryById(id);
        }

        public List<ProductCategoryViewModel> SearchModel(SearchViewModel searchViewModel)
        {
            return _repositoryDoamin.Search(searchViewModel);
        }
    }
}
