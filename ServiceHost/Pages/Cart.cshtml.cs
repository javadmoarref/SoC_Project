using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using _01_SogandShopQuery.Contracts.Product;
using _0_Framework.Application;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        
        public List<CartItem> CartItems;
        public const string CookieName = "cart-Items";
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
            CartItems = new List<CartItem>();
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.CalculateTotalItemPrice();
            }
            CartItems = _productQuery.CheckInventoryStatus(cartItems);
        }
        //public IActionResult OnGetRemoveFromCart(long id)
        //{
        //var serializer = new JavaScriptSerializer();
        //var value = Request.Cookies[CookieName];
        //Response.Cookies.Delete(CookieName);
        //var cartItems = serializer.Deserialize<List<CartItem>>(value);
        //var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
        //cartItems.Remove(itemToRemove);
        //var options = new CookieOptions { Expires = DateTime.Now.AddDays(-2)};
        //Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
        //    return RedirectToPage("/Cart");
        //}

        public IActionResult OnGetUpdateCart()
        {
            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.TotalItemPrice = double.Parse(item.UnitPrice) * item.Count;
            }
            CartItems = _productQuery.CheckInventoryStatus(cartItems);
            if (CartItems.Any(x => !x.IsInStock))
            {
                ViewData["Message"] = ApplicationMessage.InvalidCountOfProduct;
                return Page();
            }
            else
            {
                return RedirectToPage("/CheckOut");
            }
        }
    }
}
