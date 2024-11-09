using AccountManagement.Application.Contracts.Account;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ContactUS;

namespace ServiceHost.Pages
{
    public class ContactUsModel : PageModel
    {
        public ContactUSViewModel ContactUs { get; set; }
        private readonly IContactUsApplication _contactUsApplication;

        public ContactUsModel(IContactUsApplication contactUsApplication)
        {
            _contactUsApplication = contactUsApplication;
        }

        public void OnGet()
        {
            ContactUs = _contactUsApplication.GetList().FirstOrDefault(); 
        }
    }
}
