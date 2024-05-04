using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }
        public SelectList Products { get; set; }
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _colleagueDiscountApplication.Remove(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _colleagueDiscountApplication.Restore(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
