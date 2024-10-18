using _01_SogandShopQuery.Contracts.ArticleCategory;
using _01_SogandShopQuery.Contracts.ProductCategory;
using ShopManagement.Domain.OrderAgg;

namespace _01_SogandShopQuery.Model;

public class MenuModel
{
    public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    public List<ProductCategoryQueryModel> ProductCategories { get; set; }
}