using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_FrameWork.BaseClass
{
   public class MaxFileSizeAttribute:ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxfilesize;
        public MaxFileSizeAttribute(int maxfilesize)
        {
            _maxfilesize = maxfilesize;
        }
        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
                if(file == null) return true;
           return file.Length <= _maxfilesize;
              
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-maxFileSize", ErrorMessage);
        }
    }
}
