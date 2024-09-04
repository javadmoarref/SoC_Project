using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login command)
        {
           var result= _accountApplication.Login(command);
           if (result.IsSuccedded)
           {
               return RedirectToPage("/Index");
           }

           ViewData["Message"]=result.Message;
           return Page();
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }
    }
}
