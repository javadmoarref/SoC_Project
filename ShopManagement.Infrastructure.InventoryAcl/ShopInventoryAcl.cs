using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool IncreaseToInventory(List<OrderItem> items)
        {
            var command = new List<IncreaseInventory>();
            foreach (var orderItem in items)
            {
                var item = new IncreaseInventory(orderItem.ProductId, orderItem.Count, "لغو سفارش مشتری");
                command.Add(item);
            }
            var result = new OperationResult();
            result = _inventoryApplication.Increase(command);
            return result.IsSuccedded;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = new List<ReduceInventory>();
            foreach (var orderItem in items)
            {
                var item = new ReduceInventory(orderItem.ProductId, orderItem.Count, "خرید مشتری", orderItem.OrderId);
                command.Add(item);
            }
            var result = new OperationResult();
            result = _inventoryApplication.Reduce(command);
            return result.IsSuccedded;
        }
    }
}
