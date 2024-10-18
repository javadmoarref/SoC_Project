﻿namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalAmountWithDiscount { get; set; }
        public double PayAmount { get; set; }
        public double PostagePrice { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void Add(CartItem cartItem)
        {
            Items.Add(cartItem);
            TotalAmount = TotalAmount + cartItem.TotalItemPrice;
            DiscountAmount = DiscountAmount + cartItem.DiscountAmount;
            PayAmount = PayAmount + cartItem.ItemPayAmount;
        }
    }
}
