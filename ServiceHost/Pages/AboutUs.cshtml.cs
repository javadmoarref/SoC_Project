using AccountManagement.Application.Contracts.Account;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.AboutUs;

namespace ServiceHost.Pages
{
    public class AboutUsModel : PageModel
    {
        public AboutUsViewModel AboutUs { get; set; }
        private readonly IAboutUsApplication _aboutUsApplication;

        public AboutUsModel(IAboutUsApplication aboutUsApplication)
        {
            _aboutUsApplication = aboutUsApplication;
        }

        public void OnGet()
        {
            AboutUs = _aboutUsApplication.GetList().FirstOrDefault();
        }
    }
}
