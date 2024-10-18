using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Postage;

namespace ServiceHost.Areas.Administration.Pages.Shop.Postages
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditPostage Command { get; set; }
        private readonly IPostageApplication _postageApplication;

        public EditModel(IPostageApplication postageApplication)
        {
            _postageApplication = postageApplication;
        }

        public void OnGet(long id)
        {
            Command = _postageApplication.GetPostage(id);
        }

        public IActionResult OnPost(EditPostage command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result=_postageApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = _postageApplication.GetPostage(command.Id);
            return Page();
        }
    }
}
