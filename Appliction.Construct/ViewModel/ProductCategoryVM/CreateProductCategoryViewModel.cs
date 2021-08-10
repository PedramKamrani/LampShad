using _0_FrameWork.BaseClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.ProductCategoryVM
{
    public class CreateProductCategoryViewModel
    {

        [Required(ErrorMessage =ValidationMessages.IsRequired )]
        public string Name { get; set; }
        public string Description { get; set; }
        [MaxFileSize(3* 1024*1024,ErrorMessage ="حجم فایل بیشتر از٣مگا بایت است")]
        
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }

    }
}
