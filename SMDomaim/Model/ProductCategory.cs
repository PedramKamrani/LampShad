using _0_FrameWork.BaseClass;
using SMDomaim.Model.ProuductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDomaim.Model
{
    public class ProductCategory : EntityBase
    {
       
        public ProductCategory(string name, string description, string picture,
          string pictureAlt, string pictureTitle, string keywords, string metaDescription,
          string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            Creation = DateTime.Now;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
       public List<Product> Products { get; private set; }

      

        public void Edit(string name, string description, string picture,
            string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            string slug)
        {
            Name = name;
            Description = description;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            Creation = DateTime.Now;
        }
    }
}
