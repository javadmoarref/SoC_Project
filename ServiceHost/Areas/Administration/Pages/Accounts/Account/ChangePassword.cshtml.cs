using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class ChangePasswordModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ChangePassword Command { get; set; }
        private readonly IAccountApplication _accountApplication;

        public ChangePasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }


        public void OnGet(long id)
        {
            Command = new ChangePassword() { Id = id };
        }

        public IActionResult OnPost(ChangePassword command)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["Message"] = ApplicationMessage.InvalidModelState;
            //    return Page();
            //}
            var result=_accountApplication.ChangePassword(command);
            if (result.IsSuccedded)
            {
                Message=result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] =ApplicationMessage.PasswordNotMatch;
            Command = new ChangePassword() { Id = command.Id };
            return Page();
        }
    }
}
