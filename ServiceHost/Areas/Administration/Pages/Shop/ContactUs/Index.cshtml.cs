using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ContactUS;

namespace ServiceHost.Areas.Administration.Pages.Shop.ContactUs
{
    public class IndexModel : PageModel
    {
        public List<ContactUSViewModel> ContactUsList { get; set; }
        private readonly IContactUsApplication _contactUsApplication;

        public IndexModel(IContactUsApplication contactUsApplication)
        {
            _contactUsApplication = contactUsApplication;
        }

        public void OnGet()
        {
            ContactUsList =_contactUsApplication.GetList();
        }

    }
}
