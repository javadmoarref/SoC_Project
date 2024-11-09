using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ContactUS;
using ShopManagement.Application.Contracts.Postage;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ContactUs
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreateContactUs Command { get; set; }
        private readonly IContactUsApplication _contactUsApplication;

        public CreateModel(IContactUsApplication contactUsApplication)
        {
            _contactUsApplication = contactUsApplication;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(CreateContactUs command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result = _contactUsApplication.Create(command);
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
