using _0_Framework.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ServiceHost.Pages
{
    [Authorize]
    public class OrderTrackingModel : PageModel
    {
        public OrderViewModel Order { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
        private readonly IOrderApplication _orderApplication;
        private readonly IAuthHelper _authHelper;

        public OrderTrackingModel(IOrderApplication orderApplication, IAuthHelper authHelper)
        {
            _orderApplication = orderApplication;
            _authHelper = authHelper;
        }

        public void OnGet()
        {
            var currentAccountId = _authHelper.CurrentAccountId();
            Order = _orderApplication.GetOrderByCurrentAccountId(currentAccountId);
            Items = _orderApplication.GetItems(Order.Id);
        }

        public IActionResult OnGetOrderCanceled(long id)
        {
            _orderApplication.Cancel(id);
           return RedirectToPage("/OrderTracking");
        }
    }
}
