using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.AboutUs;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.AboutUs
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<AboutUsViewModel> AboutUsList { get; set; }
        private readonly IAboutUsApplication _aboutUsApplication;

        public IndexModel(IAboutUsApplication aboutUsApplication)
        {
            _aboutUsApplication = aboutUsApplication;
        }

        public void OnGet()
        {
           AboutUsList=_aboutUsApplication.GetList();
        }
    }
}
