using _0_FrameWork.BaseClass;
using Application.Contract.ViewModel.ColleagueDiscountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCountDoamin.ColleagueDiscountAgg
{
   public interface IColleagueRepository:IRepositoryBaseClass<long,ColleagueDiscount>
    {
        EditcolleagueDiscountViewModel GetDetails(long id);

        List<ColleagueDiscounViewModel> search(ColleagueSearchModel searchModel);
    }
}
