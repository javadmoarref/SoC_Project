using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Postage;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Postages
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreatePostage Command { get; set; }
        private readonly IPostageApplication _postageApplication;

        public CreateModel(IPostageApplication postageApplication)
        {
            _postageApplication = postageApplication;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(CreatePostage command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result = _postageApplication.Create(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            return Page();
        }
    }
}
