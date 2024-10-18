namespace ShopManagement.Application.Contracts.Order
{
    public class OrderItemViewModel
    {
        public long ProductId { get;  set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Count { get;  set; }
        public double UnitPrice { get;  set; }
        public int DiscountRate { get;  set; }
        public long OrderId { get;  set; }
        public OrderViewModel Order { get;  set; }
    }
}
