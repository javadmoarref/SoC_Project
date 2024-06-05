namespace _01_SogandShopQuery.Contracts.Product;

public interface IProductQuery
{
    ProductQueryModel GetProductBy(string id);
    List<ProductQueryModel> GetLatestArrivals();

}