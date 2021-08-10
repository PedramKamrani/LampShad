using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.ProductVM;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appliction.Construct.ViewModel.ProdcutPictureVM
{
   public class CreateProductPicture
    {
       
        public long ProductId { get; set; }

      [MaxFileSizeAttribute(3*1024*1024)]
      
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage =ValidationMessages.IsRequired )]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
      public  List<ProductViewModel> Products { get; set; }
        
    }
}
