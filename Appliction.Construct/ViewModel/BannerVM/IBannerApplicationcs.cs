using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.BannerVM
{
   public interface IBannerApplicationcs
    {
        OperationResult Edit(EditBannerViewModel command);
        EditBannerViewModel GetDetails(long id);
        List<BannerViewModel> GetAllBanner();
    }
}
