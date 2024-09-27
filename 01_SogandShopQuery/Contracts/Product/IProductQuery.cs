using ShopManagement.Application.Contracts.Order;

namespace _01_SogandShopQuery.Contracts.Product;

public interface IProductQuery
{
    ProductQueryModel GetProductBy(string id);
    List<ProductQueryModel> GetLatestArrivals();
    List<ProductQueryModel> Search(string value);
    List<CartItem> CheckInventoryStatus(List<CartItem> catItems);
}