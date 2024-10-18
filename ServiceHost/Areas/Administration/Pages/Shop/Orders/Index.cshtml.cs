using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;
namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public OrderSearchModel SearchModel { get; set; }
        public List<OrderViewModel> Orders { get; set; }

        private readonly IOrderApplication _orderApplication;

        public IndexModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Orders = _orderApplication.Search(searchModel);
        }

        public IActionResult OnGetRemoveOrder(long id)
        {
            _orderApplication.RemoveOrder(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetPayUnSuccess(long id)
        {
            _orderApplication.PayUnSuccess(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetOrderCancel(long id)
        {
            _orderApplication.OrderCancel(id);
            return RedirectToPage("./Index");
        }
    }
}
