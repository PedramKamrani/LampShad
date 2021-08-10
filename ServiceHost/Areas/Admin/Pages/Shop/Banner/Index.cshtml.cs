using Appliction.Construct.ViewModel.BannerVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Admin.Pages.Shop.Banner
{
    public class IndexModel : PageModel
    {
        private readonly IBannerApplicationcs  _Banner;

        [TempData]
        public string Message { get; set; }
        public  List<BannerViewModel> BannerList;
        public IndexModel(IBannerApplicationcs banner)
        {
            _Banner = banner;
        }
        public void OnGet()
        {
            BannerList = _Banner.GetAllBanner();
        }
      
        public IActionResult OnGetEdit(long id)
        {
            var command = _Banner.GetDetails(id);
            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditBannerViewModel command)
        {
           var result= _Banner.Edit(command);
            return new JsonResult(result);
        }

        
       
    }
}
