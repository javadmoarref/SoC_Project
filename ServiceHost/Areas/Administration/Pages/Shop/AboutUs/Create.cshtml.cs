using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.AboutUs;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.AboutUs
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreateAboutUs Command { get; set; }
        private readonly IAboutUsApplication _aboutUsApplication;

        public CreateModel(IAboutUsApplication aboutUsApplication)
        {
            _aboutUsApplication = aboutUsApplication;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost(CreateAboutUs command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result=_aboutUsApplication.Create(command);
            if (result.IsSuccedded)
            {
                Message=result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] =result.Message;
            return Page();
        }
    }
}
