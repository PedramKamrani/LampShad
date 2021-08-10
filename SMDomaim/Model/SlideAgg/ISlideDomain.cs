using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.SlideViewModel;
using System.Collections.Generic;

namespace SMDomaim.Model.SlideAgg
{
   public interface ISlideDomain:IRepositoryBaseClass<long,Slide>
    {
        EditSlideViewModel GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
