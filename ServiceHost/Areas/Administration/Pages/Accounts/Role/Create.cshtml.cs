using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreateRole Command { get; set; }
        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(CreateRole command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result=_roleApplication.Create(command);
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
