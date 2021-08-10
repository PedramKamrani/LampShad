using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.BannerVM
{
   public class BannerViewModel
    {
        public long Id { get; set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string Heading { get;  set; }
        public string Text { get;  set; }
        public string Link { get;  set; }
    }
}
