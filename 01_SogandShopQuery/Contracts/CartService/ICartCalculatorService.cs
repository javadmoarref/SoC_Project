using ShopManagement.Application.Contracts.Order;

namespace _01_SogandShopQuery.Contracts.CartService
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }  
}
