using System.Collections.Generic;
using _0_FrameWork.RepositoryBase;
using Appliction.Construct.ViewModel.SlideViewModel;
using ConfigurationLayer.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Shop.Slide
{
    public class IndexModel : PageModel
    {
        private readonly ISlideApplication _slide;

        [TempData]
        public string Message { get; set; }
        public  List<SlideViewModel> slideList;
        public IndexModel(ISlideApplication slide)
        {
            _slide = slide;
        }
        public void OnGet()
        {
            slideList = _slide.GetList();
        }
      
        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        public JsonResult OnPostCreate(CreateSlideViewModel command)
        {
           var result= _slide.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var command = _slide.GetDetails(id);
            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditSlideViewModel command)
        {
           var result= _slide.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _slide.Remove(id);
            if(result.Sussecced)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            var result = _slide.Restore(id);
            if(result.Sussecced)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
