using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.ViewModel.CustomerDiscountVM
{
   public interface ICustomerDiscountApplication
    {
       OperationResult Define(DefineCustomerDiscount command);
        OperationResult Edit(EditCustoemrDiscount command);
        EditCustoemrDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}

