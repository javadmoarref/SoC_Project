using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditSlide Command { get; set; }
        private readonly ISlideApplication _slideApplication;

        public EditModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet(long id)
        {
            Command = _slideApplication.GetDetails(id);
        }

        public IActionResult OnPost(EditSlide command)
        {
            var result=_slideApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            Message= result.Message;
            Command = _slideApplication.GetDetails(command.Id);
            return Page();
        }
    }
}
