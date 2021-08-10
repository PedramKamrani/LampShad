using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.BannerVM;
using System.Collections.Generic;

namespace SMDomaim.Model.BannerAgg
{
   public interface IBannerRepository:IRepositoryBaseClass<long,Banner>
    {
        List<BannerViewModel> GetAllBanner();
        EditBannerViewModel GetDetails(long id);
    }
}
