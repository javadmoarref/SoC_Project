using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscounts
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditColleagueDiscount Command { get; set; }
        public SelectList Products { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public EditModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }


        public void OnGet(long id)
        {
            Command = _colleagueDiscountApplication.GetDetails(id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnPost(EditColleagueDiscount command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result = _colleagueDiscountApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = _colleagueDiscountApplication.GetDetails(command.Id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            return Page();
        }
    }
}
