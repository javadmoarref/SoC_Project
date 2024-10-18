using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_SogandShopQuery.Contracts.CartService;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Infrastructure.EFCore;

namespace _01_SogandShopQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;
        private readonly ShopContext _shopContext;

        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper,
            ShopContext shopContext)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
            _shopContext = shopContext;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x=>!x.IsRemoved)
                .Select(x => new { x.DiscountRate, x.ProductId}).ToList();
            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();
            var currentAccountRole = _authHelper.CurrentAccountRole();
            var postagePrice = (_shopContext.Postages.Where(x=>x.PostagePrice>0).FirstOrDefault()).PostagePrice;
            
            if (currentAccountRole == Roles.ColleagueUser)
            {
                foreach (var cartItem in cartItems)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (colleagueDiscount != null)
                    {
                        cartItem.DiscountRate = colleagueDiscount.DiscountRate;
                    }
                    cartItem.DiscountAmount = ((cartItem.TotalItemPrice * cartItem.DiscountRate) / 100);
                    cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                    cart.Add(cartItem);
                }
            }
            else
            {
                foreach (var cartItem in cartItems)
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (customerDiscount != null)
                    {
                        cartItem.DiscountRate = customerDiscount.DiscountRate;
                    }
                    cartItem.DiscountAmount = ((cartItem.TotalItemPrice * cartItem.DiscountRate) / 100);
                    cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                    cart.Add(cartItem);
                }
            }
            cart.TotalAmountWithDiscount = cart.PayAmount;
            cart.PostagePrice = postagePrice;
            cart.PayAmount = cart.PayAmount + postagePrice;
            return cart;
        }
    }
}
