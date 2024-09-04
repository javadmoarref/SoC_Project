using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditAccount Command { get; set; }
        public SelectList Roles { get; set; }
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public EditModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(long id)
        {
            Command = _accountApplication.GetDetails(id);
            Roles = new SelectList(_roleApplication.GetList(), "Id", "Name");
        }

        public IActionResult OnPost(EditAccount command)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["Message"] = ApplicationMessage.InvalidModelState;
            //    return Page();
            //}
            var result=_accountApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = _accountApplication.GetDetails(command.Id);
            Roles = new SelectList(_roleApplication.GetList(), "Id", "Name");
            return Page();
        }
    }
}
