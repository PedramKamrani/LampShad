using _0_FrameWork.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDomaim.Model.BannerAgg
{
   public class Banner:EntityBase
    {
       
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Heading { get; private set; }
        public string Text { get; private set; }
        public string Link { get; private set; }


        public Banner(string picture, string pictureAlt, string pictureTitle, 
            string heading, string text, string link)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
           
            Text = text;
            Link = link;
        }
        public void Edit(string picture, string pictureAlt, string pictureTitle
            , string heading,  string text, string link)
        {
            if(!string.IsNullOrWhiteSpace(picture))
            Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Text = text;
            Link = link;
        }


    }
}
