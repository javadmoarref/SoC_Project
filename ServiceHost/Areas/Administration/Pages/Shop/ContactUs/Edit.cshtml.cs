using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ContactUS;

namespace ServiceHost.Areas.Administration.Pages.Shop.ContactUs
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditContactUs Command { get; set; }
        
        private readonly IContactUsApplication _contactUsApplication;

        public EditModel(IContactUsApplication contactUsApplication)
        {
            _contactUsApplication = contactUsApplication;
        }

        public void OnGet(long id)
        {
            Command = _contactUsApplication.GetDetails(id);
        }

        public IActionResult OnPost(EditContactUs command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result=_contactUsApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = _contactUsApplication.GetDetails(command.Id);
            return Page();
        }
    }
}
