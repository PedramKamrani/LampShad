using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.ProductVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDomaim.Model.ProuductAgg
{
   public interface IProductRepositoryDoamin: IRepositoryBaseClass<long,Product>
    {
        EditProductViewModel GetDetail(long id);
        List<ProductViewModel> GetAllProducts();
        List<ProductViewModel> SearchProduct(ProductSearchModelViewModel searchmodel);
    }
}
