namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        void PlaceOrder(Cart cart);
        void PaymentSucceeded(long orderId,long refId);
        void SetPostalCode(long orderId,long postalCode);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        OrderViewModel GetOrderById(long id);
        List<OrderItemViewModel> GetItems(long orderId);
        void RemoveOrder(long id);
        void PayUnSuccess(long id);
        void OrderCancel(long id);
    }
}
