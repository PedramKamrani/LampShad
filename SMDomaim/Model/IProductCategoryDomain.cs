using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.ProductCategoryVM;
using System.Collections.Generic;

namespace SMDomaim.Model
{
   public interface IProductCategoryDomain: IRepositoryBaseClass<long,ProductCategory>
    {
        EditProductcategoryViewModel GetDetails(long id);
        string GetSlugCategoryById(long id);
        List<ProductCategoryViewModel> Search(SearchViewModel searchViewModel);
        List<ListProductCategoryViewModel> CategoryList();
    }
}
