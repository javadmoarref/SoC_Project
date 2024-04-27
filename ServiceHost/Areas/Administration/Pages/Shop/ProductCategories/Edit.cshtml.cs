using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditProductCategory Command { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;

        public EditModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(long id)
        {
            Command = _productCategoryApplication.GetDetails(id);
        }

        public IActionResult OnPost(EditProductCategory command)
        {
            var result=_productCategoryApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            Message = result.Message;
            Command = _productCategoryApplication.GetDetails(command.Id);
            return Page();
        }
    }
}
