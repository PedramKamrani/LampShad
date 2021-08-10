using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.SlideViewModel
{
   public interface ISlideApplication
    {
        OperationResult Create(CreateSlideViewModel command);
        OperationResult Edit(EditSlideViewModel command);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditSlideViewModel GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
