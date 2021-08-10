using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagmentApplication.Contract.ArticleVM
{
  public class ArticleViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Picture { get; set; }
        public string PublishDate { get; set; }
        public string Category { get; set; }
        public long CategoryId { get; set; }
    }
}
