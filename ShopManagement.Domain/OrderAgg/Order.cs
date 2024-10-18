using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order:EntityBase
    {
        public long AccountId { get; private set; }
        public string AccountName { get; private set; }
        public string AccountMobile { get; private set; } 
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public bool IsPayUnSuccess { get; private set; }
        public bool IsOrderCanceled { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public long PostalCode { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId,string accountName, string accountMobile,
            double totalAmount, double discountAmount,double payAmount)
        {
            AccountId = accountId;
            AccountName = accountName;
            AccountMobile = accountMobile;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IsPaid = false;
            IsCanceled=false;
            IsPayUnSuccess=false;
            IsOrderCanceled=false;
            RefId = 0;
            Items = new List<OrderItem>();
        }

        protected Order()
        {
        }

        public void PaymentSucceeded(long refId)
        {
            IsPaid = true;
            if (refId != 0)
            {
                RefId = refId;
            }
        }

        public void SetPostalCode(long postalCode)
        {
            if (postalCode != 0)
            {
                PostalCode = postalCode;
            }
        }

        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo = number;
        } 

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void OrderCancel()
        {
            IsOrderCanceled = true;
        }

        public void PayUnSuccess()
        {
            IsPayUnSuccess = true;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }
    }
}
