using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository:IRepository<long,Order>
    {
        OrderViewModel GetOrderById(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long orderId);
        OrderViewModel GetOrderByCurrentAccountId(long currentAccountId);
        double GetAmountBy(long id);
    }
}
