using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_FrameWork.BaseClass;
using _01_QueryLamshade.Contracts.Product;
using CommenetManagmenrt.Infractracer.EFCore;
using Comment.Managment.Cantract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQueryModel;
        private readonly ICommentApplication _commentApplication;
        public ProductQueryModel Product;
        public ProductModel(IProductQuery productQueryModel,ICommentApplication commentApplication)
        {
            _productQueryModel = productQueryModel;
            _commentApplication = commentApplication;
        }
        public void OnGet(string id)
        {
            Product = _productQueryModel.GetProductDetails(id);
        }

        public IActionResult OnPost(AddComment command, string productSlug)
        {
            command.Type = CommentType.Product;
            OperationResult result = _commentApplication.Add(command);
            return RedirectToPage("/Product", new { Id = productSlug });
        }
    }
}
