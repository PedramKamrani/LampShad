using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using Appliction.Construct.ViewModel.SlideViewModel;
using SMDomaim.Model.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infractrucuer.EFCore.Repository
{
   public class SlideRepository:RepositoryBaseClass<long,Slide>,ISlideDomain
    {
        private readonly ShopContext _context;
        public SlideRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public EditSlideViewModel GetDetails(long id)
        {
            return _context.Slides.Select(s => new EditSlideViewModel
            {
                id = s.Id,
                IsRemoved = s.IsRemoved,
                BtnText = s.BtnText,
                Link = s.Link,
                PictureAlt = s.PictureAlt,
                PictureTitle = s.PictureTitle,
                Text = s.Text,
                Title = s.Title,
                Heading = s.Heading,

            }).FirstOrDefault(p => p.id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(p => new SlideViewModel
            {
                Id=p.Id,
                CreationDate=p.Creation.ToFarsi(),
                Heading=p.Heading,
                IsRemoved=p.IsRemoved,
                Picture=p.Picture,
                Title=p.Title
            }).OrderBy(p=>p.Id).ToList();
        }
    }
}
