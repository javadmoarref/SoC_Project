using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class ItemModel : PageModel
    {
        public OrderViewModel Order { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
        private readonly IOrderApplication _orderApplication;

        public ItemModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public void OnGet(long id)
        {
            Order = _orderApplication.GetOrderById(id);
            Items=_orderApplication.GetItems(id);
        }

    }
}
