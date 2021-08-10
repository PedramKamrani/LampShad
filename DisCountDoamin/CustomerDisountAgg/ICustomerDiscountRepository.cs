using _0_FrameWork.BaseClass;
using Application.Contract.ViewModel.CustomerDiscountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCountDoamin.CustomerDisountAgg
{
   public interface ICustomerDiscountRepository:IRepositoryBaseClass<long,CustomerDiscount>
    {
        EditCustoemrDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}
