namespace InventoryManagement.Application.Contracts.Inventory;

public class IncreaseInventory
{
    public long InventoryId { get; set; }
    public long Count { get; set; }
    public string Description { get; set; }

    public IncreaseInventory(long inventoryId, long count, string description)
    {
        InventoryId = inventoryId;
        Count = count;
        Description = description;
    }

    public IncreaseInventory()
    {
    }
}