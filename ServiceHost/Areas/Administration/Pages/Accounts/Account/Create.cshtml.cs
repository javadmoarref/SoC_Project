using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreateAccount Command { get; set; }
        public SelectList Roles { get; set; }
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;


        public CreateModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            Roles = new SelectList(_roleApplication.GetList(), "Id", "Name");
        }

        public IActionResult OnPost(CreateAccount command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result=_accountApplication.Create(command);
            if (result.IsSuccedded)
            {
                Message=result.Message;
                return RedirectToPage("Index");
            }
            Roles = new SelectList(_roleApplication.GetList(), "Id", "Name");
            ViewData["Message"] =result.Message;
            return Page();
        }
    }
}
