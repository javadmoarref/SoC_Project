using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IShopInventoryAcl _shopInventoryAcl;

        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper,
            IConfiguration configuration, IShopInventoryAcl shopInventoryAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _configuration = configuration;
            _shopInventoryAcl = shopInventoryAcl;
        }


        public void PlaceOrder(Cart cart)
        {
            var currentAccountInfo = _authHelper.CurrentAccountInfo();
            var order = new Order(currentAccountInfo.Id, currentAccountInfo.Fullname, currentAccountInfo.Mobile, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);
            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Name, cartItem.Picture, cartItem.Count, double.Parse(cartItem.UnitPrice), cartItem.DiscountRate);
                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
        }

        //public long PlaceOrder(Cart cart)
        //{
        //    var currentAccountInfo = _authHelper.CurrentAccountInfo();
        //    var order = new Order(currentAccountInfo.Id, currentAccountInfo.Fullname, currentAccountInfo.Mobile, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);
        //    foreach (var cartItem in cart.Items)
        //    {
        //        var orderItem = new OrderItem(cartItem.Id, cartItem.Name, cartItem.Picture, cartItem.Count, double.Parse(cartItem.UnitPrice), cartItem.DiscountRate);
        //        order.AddItem(orderItem);
        //    }

        //    _orderRepository.Create(order);
        //    _orderRepository.SaveChanges();
        //    return order.Id;
        //}

        public void PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            var symbol = _configuration.GetSection("Symbol").Value;
            var issueTrackingNo = CodeGenerator.Generate(symbol);
            order.SetIssueTrackingNo(issueTrackingNo);
            if (_shopInventoryAcl.ReduceFromInventory(order.Items))
            {
                _orderRepository.SaveChanges();
            }
        }

        //public string PaymentSucceeded(long orderId, long refId)
        //{
        //    var order = _orderRepository.Get(orderId);
        //    order.PaymentSucceeded(refId);
        //    var symbol = _configuration.GetSection("Symbol").Value;
        //    var issueTrackingNo = CodeGenerator.Generate(symbol);
        //    order.SetIssueTrackingNo(issueTrackingNo);
        //    if (_shopInventoryAcl.ReduceFromInventory(order.Items))
        //    {
        //        _orderRepository.SaveChanges();
        //    }
        //    return issueTrackingNo;
        //}


        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }

        public OrderViewModel GetOrderById(long id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            return _orderRepository.GetItems(orderId);
        }

        public OrderViewModel GetOrderByCurrentAccountId(long accountId)
        {
            return _orderRepository.GetOrderByCurrentAccountId(accountId);
        }

        public void RemoveOrder(long id)
        {
            var order=_orderRepository.Get(id);
            _orderRepository.Remove(order);
            _orderRepository.SaveChanges();
        }

        public void SetPostalCode(long orderId,long postalCode)
        {
            var order = _orderRepository.Get(orderId);
            order.SetPostalCode(postalCode);
            _orderRepository.SaveChanges();
        }

        public void PayUnSuccess(long id)
        {
            var order = _orderRepository.Get(id);
            order.PayUnSuccess();
            _orderRepository.SaveChanges();
        }

        public void OrderCancel(long id)
        {
            var order = _orderRepository.Get(id);
            order.OrderCancel();
            if (_shopInventoryAcl.IncreaseToInventory(order.Items))
            {
                _orderRepository.SaveChanges();
            }
        }

        public void Cancel(long id)
        {
            var order=_orderRepository.Get(id);
            order.Cancel();
            _orderRepository.SaveChanges();
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }
    }
}
