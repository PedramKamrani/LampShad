using _0_FrameWork.BaseClass;
using BlogManagmentApplication.Contract.ArticleCategoryVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagmentDomain.ArticleCategoryDomain
{
   public interface IAricleCategoryRepository:IRepositoryBaseClass<long,ArticleCategory>
    {
        string GetSlugBy(long id);
        EditArticleCategory GetDetails(long id);
        List<ArticleCategoryViewModel> GetArticleCategories();
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}
