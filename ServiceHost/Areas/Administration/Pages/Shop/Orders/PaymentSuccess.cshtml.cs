using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class PaymentSuccessModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public OrderViewModel Command { get; set; }
        private IOrderApplication _orderApplication;

        public PaymentSuccessModel(IOrderApplication orderApplication)
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
            if (command.RefId == 0)
            {
                ViewData["Message"]=ApplicationMessage.EnterRefId;
                Command = _orderApplication.GetOrderById(command.Id);
                return Page();
            }
            var refId = command.RefId;
            _orderApplication.PaymentSucceeded(orderId,refId);
            return RedirectToPage("./Index");
        }
    }
}
