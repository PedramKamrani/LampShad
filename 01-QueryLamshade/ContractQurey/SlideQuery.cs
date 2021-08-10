using _01_QueryLamshade.Contracts.Slide;
using Infractrucuer.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_QueryLamshade.ContractQurey
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;
        public SlideQuery(ShopContext context)
        {
            _context = context;
        }
        public List<SlideQueryModel> GetSlides()
        {
           var slide= _context.Slides.Select(p => new SlideQueryModel
            {
                BtnText=p.BtnText,
                Heading=p.Heading,
                Link=p.Link,
                Picture=p.Picture,
                PictureAlt=p.PictureAlt,
                PictureTitle=p.PictureTitle,
                Text=p.Text,
                Title=p.Title
            });
            return slide.ToList();
        }
    }
}
