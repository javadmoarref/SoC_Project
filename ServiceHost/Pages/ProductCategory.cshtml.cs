using _01_SogandShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductCategoryQueryModel Category { get; set; }
        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string id)
        {
            Category = _productCategoryQuery.GetProductCategoryWithProductsBy(id);
        }
    }
}
