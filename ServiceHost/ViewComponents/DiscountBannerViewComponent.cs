using Appliction.Construct.ViewModel.BannerVM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.ViewComponents
{
    public class DiscountBannerViewComponent : ViewComponent
    {
        private readonly IBannerApplicationcs _bannerApplication;
        public DiscountBannerViewComponent(IBannerApplicationcs bannerApplication)
        {
            _bannerApplication = bannerApplication;
        }

        public IViewComponentResult Invoke()
        {
            var banner = _bannerApplication.GetAllBanner();
            return View(banner);
        }
    }
}
