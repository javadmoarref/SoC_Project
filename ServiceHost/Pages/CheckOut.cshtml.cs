using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_SogandShopQuery.Contracts.CartService;
using _01_SogandShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckOutModel : PageModel
    {
        public Cart Cart { get; set; }
        public const string CookieName = "cart-Items";
        public bool ReturnToMySite { get; set; }
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IAuthHelper _authHelper;

        public CheckOutModel(ICartCalculatorService cartCalculatorService, ICartService cartService,
            IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory,
            IAuthHelper authHelper)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
        }

        public void OnGet()
        {

            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            if (value == null)
            {
                Cart = new Cart();
            }
            else
            {
                var cartItems = serializer.Deserialize<List<CartItem>>(value);
                foreach (var item in cartItems)
                {
                    item.CalculateTotalItemPrice();
                }

                Cart = _cartCalculatorService.ComputeCart(cartItems);
                _cartService.Set(Cart);
            }
        }

        public IActionResult OnGetPay()
        {
            var cart = _cartService.Get();
            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
            {
                return RedirectToPage("/Cart");
            }
            _orderApplication.PlaceOrder(cart);
            return Redirect($"https://zarinp.al/sogandsite");
            //var orderId = _orderApplication.PlaceOrder(cart);
            //var accountInfo = _authHelper.CurrentAccountInfo();
            //var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
            //    cart.PayAmount.ToString(CultureInfo.InvariantCulture), accountInfo.Mobile, "", "خرید از درگاه لوازم آرایشی سوگند", orderId);
            //return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }

        //public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
        //[FromQuery] long oId)
        //{
        //    var orderAmount = _orderApplication.GetAmountBy(oId);
        //    var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString(CultureInfo.InvariantCulture));
        //    var result = new PaymentResult();
        //    if (status == "OK" && verificationResponse.Status >= 100)
        //    {
        //        var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
        //        result = result.Succeeded(ApplicationMessage.PayIsSuccessFull, issueTrackingNo);
        //        return RedirectToPage("/PaymentResult", result);
        //    }
        //    else
        //    {
        //        _orderApplication.PayUnSuccess(oId);
        //        result = result.Failed(ApplicationMessage.PayIsWrong);
        //        return RedirectToPage("/PaymentResult", result);
        //    }
        //}
    }
}
