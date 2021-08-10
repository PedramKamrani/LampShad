using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.ProdcutPictureVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDomaim.Model.ProductPictureAgg
{
   public interface IProductPictureRepositoryDomain:IRepositoryBaseClass<long,ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        ProductPicture GetWithProductAndCategory(long id);

        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    
}
}
