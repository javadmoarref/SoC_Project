using _01_SogandShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductQuery _productQuery;

        public IndexModel(ILogger<IndexModel> logger, IProductQuery productQuery)
        {
            _logger = logger;
            _productQuery = productQuery;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetWishList(string id)
        {
            var product = _productQuery.GetProductBy(id);
            return Partial("./WishList", product);
        }

        public IActionResult OnGetCartAdd(string id)
        {
            var product = _productQuery.GetProductBy(id);
            return Partial("./CartAdd", product);
        }

        //public IActionResult OnGetQuickViewDetails()
        //{
        //    return Partial("./QuickViewDetails");
        //}
    }
}