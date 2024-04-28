using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreateSlide Command { get; set; }
        private readonly ISlideApplication _slideApplication;

        public CreateModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(CreateSlide command)
        {
            var result=_slideApplication.Create(command);
            if (result.IsSuccedded)
            {
                Message=result.Message;
                return RedirectToPage("Index");
            }
            Message=result.Message;
            return Page();
        }
    }
}
