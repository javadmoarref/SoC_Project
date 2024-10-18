using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class OrderItem:EntityBase
    {
        public long ProductId { get; private set; }
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public int Count { get; private set; }
        public double UnitPrice { get; private set; }
        public int DiscountRate{ get; private set; }
        public long OrderId { get; private set; }
        public Order Order { get; private set; }

        public OrderItem(long productId,string name,string picture, int count, double unitPrice, int discountRate)
        {
            ProductId = productId;
            Name = name;
            Picture = picture;
            Count = count;
            UnitPrice = unitPrice;
            DiscountRate = discountRate;
        }

        protected OrderItem()
        {

        }
    }
}
