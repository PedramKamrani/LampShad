

using _0_FrameWork.BaseClass;
using BlogManagmentApplication.Contract.ArticleVM;
using System.Collections.Generic;

namespace BlogManagmentDomain.ArticleDomainAgg
{
   public interface IArticleRepository:IRepositoryBaseClass<long,Article>
    {
        EditArticle GetDetails(long id);
        Article GetWithCategory(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
