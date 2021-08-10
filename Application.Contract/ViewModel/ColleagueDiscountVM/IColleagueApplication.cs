using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract.ViewModel.ColleagueDiscountVM
{
   public interface IColleagueApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditcolleagueDiscountViewModel command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditcolleagueDiscountViewModel GetDetails(long id);
        List<ColleagueDiscounViewModel> Search(ColleagueSearchModel searchModel);
    }
}
