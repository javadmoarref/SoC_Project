using AccountManagement.Application.Contracts.Account;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient;
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
            //SmsIr smsIr = new SmsIr("n8iowBqCySao7eCYK0l2iLtdrmzZEyOOhm9ZCscuCBfwuNVepsF8b1eoFvlAmQPu");
            var result = _accountApplication.Login(command);
            if (result.IsSuccedded)
            {
                //var verificationSendResult = await smsIr.VerifySendAsync(command.Mobile, 337332, new VerifySendParameter[] { new VerifySendParameter("Code", "350098") });
                return RedirectToPage("/Index");
            }

            ViewData["Message"] = result.Message;
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostRegister(CreateAccount command)
        {
            var result = _accountApplication.Create(command);
            if (result.IsSuccedded)
            {
                ViewData["Message"] = result.Message;
                return Page();
            }
            ViewData["Message"] = result.Message;
            return Page();
        }
    }
}
