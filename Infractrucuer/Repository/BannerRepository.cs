using _0_FrameWork.RepositoryBase;
using Appliction.Construct.ViewModel.BannerVM;
using SMDomaim.Model.BannerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractrucuer.EFCore.Repository
{
    public class BannerRepository : RepositoryBaseClass<long, Banner>, IBannerRepository
    {
        private readonly ShopContext _context;
        public BannerRepository(ShopContext context) :base(context)
        {
            _context = context;
        }
        public List<BannerViewModel> GetAllBanner()
        {
            return _context.Banners.Select(x => new BannerViewModel
            {
                Id=x.Id,
                Heading=x.Heading,
                Link=x.Link,
                Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Text=x.Text
            }).ToList();
        }

        public EditBannerViewModel GetDetails(long id)
        {
            return _context.Banners.Select(x => new EditBannerViewModel
            {
                Id=x.Id,
                Heading=x.Heading,
                Link=x.Link,
               // Picture=x.Picture,
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Text=x.Text
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
