namespace _01_SogandShopQuery.Contracts.ProductCategory;

public interface IProductCategoryQuery
{
    List<ProductCategoryQueryModel> GetProductCategories();
    List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();

    ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
}