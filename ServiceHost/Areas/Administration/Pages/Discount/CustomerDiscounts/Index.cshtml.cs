using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CustomerDiscountSearchModel SearchModel { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }
        public SelectList Products { get; set; }
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }


    }
}
