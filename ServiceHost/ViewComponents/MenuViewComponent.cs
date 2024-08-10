using _01_SogandShopQuery.Contracts.ArticleCategory;
using _01_SogandShopQuery.Contracts.ProductCategory;
using _01_SogandShopQuery.Model;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IProductCategoryQuery _productCategoryQuery;

        public MenuViewComponent(IArticleCategoryQuery articleCategoryQuery, IProductCategoryQuery productCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel()
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
                ProductCategories = _productCategoryQuery.GetProductCategories()
            };
            return View(result);
        }
    }
}
