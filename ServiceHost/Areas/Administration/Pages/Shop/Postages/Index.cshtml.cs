using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Postage;

namespace ServiceHost.Areas.Administration.Pages.Shop.Postages
{
    public class IndexModel : PageModel
    {
        public List<PostageViewModel> Postages { get; set; }
        private readonly IPostageApplication _postageApplication;

        public IndexModel(IPostageApplication postageApplication)
        {
            _postageApplication = postageApplication;
        }

        public void OnGet()
        {
            Postages = _postageApplication.GetList();
        }

    }
}
