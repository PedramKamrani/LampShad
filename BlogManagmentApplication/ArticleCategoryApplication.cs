using _0_Framework.Application;
using _0_FrameWork.BaseClass;
using BlogManagmentApplication.Contract.ArticleCategoryVM;
using BlogManagmentDomain.ArticleCategoryDomain;
using System;
using System.Collections.Generic;

namespace BlogManagmentApplication
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IAricleCategoryRepository _aricleCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ArticleCategoryApplication(IAricleCategoryRepository aricleCategoryRepository, IFileUploader fileUploader)
        {
            _aricleCategoryRepository = aricleCategoryRepository;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateArticleCategory command)
        {
            var oprtionresult = new OperationResult();
            if (_aricleCategoryRepository.Exists(x => x.Name == command.Name))
                return oprtionresult.Failed(ApplicationMessages.DuplicatedRecord);
            string slug = command.Slug.Slugify();
            var picturename = _fileUploader.Upload(command.Picture, slug);
            var Articlecatgory = new ArticleCategory(command.Name, picturename, command.PictureAlt,
                command.PictureTitle, command.Description, command.ShowOrder
                , slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
            _aricleCategoryRepository.Create(Articlecatgory);
            _aricleCategoryRepository.SaveChanges();
           return oprtionresult.Succedded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var oprtionresult = new OperationResult();
            var Articlecatgory = _aricleCategoryRepository.Get(command.Id);

            if (Articlecatgory==null)
                return oprtionresult.Failed(ApplicationMessages.RecordNotFound);

            if (_aricleCategoryRepository.Exists(x => x.Name == command.Name&&x.Id!=command.Id))
                return oprtionresult.Failed(ApplicationMessages.DuplicatedRecord);

            string slug = command.Slug.Slugify();
            var picturename = _fileUploader.Upload(command.Picture, slug);

          Articlecatgory.Edit(command.Name, picturename, command.PictureAlt,
                command.PictureTitle, command.Description, command.ShowOrder
                , slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
           
            _aricleCategoryRepository.SaveChanges();
            return oprtionresult.Succedded();
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _aricleCategoryRepository.GetArticleCategories();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _aricleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _aricleCategoryRepository.Search(searchModel);
        }
    }
}
