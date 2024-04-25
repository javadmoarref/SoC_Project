using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreateProduct Command { get; set; }
        public SelectList ProductCategories { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public CreateModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }
        public void OnGet()
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
        }

        public IActionResult OnPost(CreateProduct command)
        {
            var result=_productApplication.Create(command);
            if (result.IsSuccedded)
            {
                Message=result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"]=result.Message;
            return Page();
        }
    }
}
