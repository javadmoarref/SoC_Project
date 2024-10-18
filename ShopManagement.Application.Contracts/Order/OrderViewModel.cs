namespace ShopManagement.Application.Contracts.Order
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public long AccountId { get;  set; }
        public string  AccountName { get;  set; }
        public string  AccountMobile { get;  set; }
        public string  AccountAddress { get;  set; }
        public string  AccountAddressPostalCode { get;  set; }
        public double TotalAmount { get;  set; }
        public double DiscountAmount { get;  set; }
        public double PayAmount { get;  set; }
        public bool IsPaid { get;  set; }
        public bool IsCanceled { get;  set; }
        public bool IsPayUnSuccess { get;  set; }
        public bool IsOrderCanceled { get;  set; }
        public string IssueTrackingNo { get;  set; }
        public long RefId { get;  set; }
        public long PostalCode { get;  set; }
        public string CreationDate { get; set; }
        public DateTime CreationDateGr { get; set; }
        public List<OrderItemViewModel> Items { get;  set; }
    }
}
