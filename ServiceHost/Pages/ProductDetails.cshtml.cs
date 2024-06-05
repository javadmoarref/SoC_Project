using _01_SogandShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public ProductQueryModel Product { get; set; }

        public ProductDetailsModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetProductBy(id);
        }
    }
}
