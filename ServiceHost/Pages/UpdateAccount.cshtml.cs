using AccountManagement.Application.Contracts.Account;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace ServiceHost.Pages
{
    public class UpdateAccountModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        public EditAccount Account { get; set; }

        public UpdateAccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(long id)
        {
            Account=_accountApplication.GetDetails(id);
        }

        public IActionResult OnPost(EditAccount account)
        {
            var result = _accountApplication.Edit(account);
            if (result.IsSuccedded)
            {
                return RedirectToPage("/OrderTracking");
            }
            Account = _accountApplication.GetDetails(account.Id);
            //Account.RoleId=command.RoleId;
            ViewData["Message"] = result.Message;
            return Page();
        }
    }
}
