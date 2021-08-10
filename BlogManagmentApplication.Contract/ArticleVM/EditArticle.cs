using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagmentApplication.Contract.ArticleVM
{
   public class EditArticle:CreateArticle
    {
        public long Id { get; set; }
    }
}
