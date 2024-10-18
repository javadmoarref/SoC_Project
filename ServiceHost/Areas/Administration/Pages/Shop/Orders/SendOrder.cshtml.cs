using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class SendOrderModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public OrderViewModel Command { get; set; }
        private IOrderApplication _orderApplication;

        public SendOrderModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public void OnGet(long id)
        {
            Command=_orderApplication.GetOrderById(id);
        }

        public IActionResult OnPost(OrderViewModel command)
        {
            var orderId = command.Id;
            if (command.PostalCode == 0)
            {
                ViewData["Message"]=ApplicationMessage.EnterPostalCode;
                Command = _orderApplication.GetOrderById(command.Id);
                return Page();
            }
            var postalCode = command.PostalCode;
            _orderApplication.SetPostalCode(orderId, postalCode);
            return RedirectToPage("./Index");
        }
    }
}
