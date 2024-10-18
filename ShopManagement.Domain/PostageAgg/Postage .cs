using _0_Framework.Domain;

namespace ShopManagement.Domain.PostageAgg
{
    public class Postage : EntityBase
    {
        public double PostagePrice { get; private set; }

        public Postage(double postagePrice)
        {
            PostagePrice = postagePrice;
        }

        public void Edit(double postagePrice)
        {
            PostagePrice = postagePrice;
        }
    }
}
