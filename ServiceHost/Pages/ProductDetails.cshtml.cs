using _0_Framework.Application;
using _01_SogandShopQuery.Contracts.Product;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore.Requirements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ServiceHost.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        
        public ProductQueryModel Product { get; set; }

        public ProductDetailsModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetProductBy(id);
        }

        public IActionResult OnPost(AddComment command, string productSlug)
        {
            command.Type = CommentType.Product; 
            var result = _commentApplication.Add(command);
            if (result.IsSuccedded)
            {
                ViewData["Message"] = ApplicationMessage.MessageSendSuccess;
            }
            else
            {
                ViewData["Message"] = ApplicationMessage.MessageSendWrong;
            }
            return RedirectToPage("/ProductDetails", new { Id = productSlug });
        }
    }
}
