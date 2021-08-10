using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.ProductVM
{
   public interface IProductAppliction
    {
        OperationResult Creat(CreateProductViewModel command);
        OperationResult Edit(EditProductViewModel command);
        OperationResult Isstack(long Id);
        OperationResult NotIsstack(long Id);
        EditProductViewModel GetDetails(long id);
      
        List<ProductViewModel> SearchProduct(ProductSearchModelViewModel searchmodel);
        List<ProductViewModel> GetAllProduct();
    }
}
