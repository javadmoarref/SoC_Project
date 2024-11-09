using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.AboutUs;

namespace ServiceHost.Areas.Administration.Pages.Shop.AboutUs
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditAboutUs Command { get; set; }
       private readonly IAboutUsApplication _aboutUsApplication;

        public EditModel(IAboutUsApplication aboutUsApplication)
        {
            _aboutUsApplication = aboutUsApplication;
        }

        public void OnGet(long id)
        {
            Command = _aboutUsApplication.GetAboutUs(id);
        }

        public IActionResult OnPost(EditAboutUs command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result=_aboutUsApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = _aboutUsApplication.GetAboutUs(command.Id);
            return Page();
        }
    }
}
