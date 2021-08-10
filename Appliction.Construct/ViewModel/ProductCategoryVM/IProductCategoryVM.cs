using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.ProductCategoryVM
{
   public interface IProductCategoryVM
    {
        OperationResult Creat(CreateProductCategoryViewModel command);
        OperationResult Edit(EditProductcategoryViewModel command);
        List<ProductCategoryViewModel> SearchModel(SearchViewModel searchViewModel);
        List<ListProductCategoryViewModel> CategoryList();
        EditProductcategoryViewModel GetDetial(long Id);
       string GetSlugCategoryById(long id);

    }
}
