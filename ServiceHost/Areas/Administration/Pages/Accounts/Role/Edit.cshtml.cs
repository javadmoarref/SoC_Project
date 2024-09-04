using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditRole Command { get; set; }
        private readonly IRoleApplication _roleApplication;

        public EditModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);
        }

        public IActionResult OnPost(EditRole command)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["Message"] = ApplicationMessage.InvalidModelState;
            //    return Page();
            //}
            var result=_roleApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = _roleApplication.GetDetails(command.Id);
            return Page();
        }
    }
}
