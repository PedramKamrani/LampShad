using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_QueryLamshade.Contracts.Product;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        public List<ProductQueryModel> Products;
        private readonly IProductQuery _productQuery;
        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }
        public void OnGet(string value)
        {
           HtmlSanitizer htmlsantaizer = new HtmlSanitizer();
            Value =htmlsantaizer.Sanitize(value);
            Products = _productQuery.Search(Value);
        }
    }
}
