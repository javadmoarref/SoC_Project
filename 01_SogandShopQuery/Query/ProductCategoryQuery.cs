using _01_SogandShopQuery.Contracts.ProductCategory;
using ShopManagement.Infrastructure.EFCore;

namespace _01_SogandShopQuery.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    private readonly ShopContext _shopContext;

    public ProductCategoryQuery(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public List<ProductCategoryQueryModel> GetProductCategories()
    {
        return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel()
        {
            Name = x.Name,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug,
            BackgroundColor = x.BackgroundColor
        }).ToList();
    }
}