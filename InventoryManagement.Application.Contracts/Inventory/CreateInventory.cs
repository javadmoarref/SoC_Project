using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory;

public class CreateInventory
{
    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    [Range(1,100000,ErrorMessage = ValidationMessage.NumberIsRequired)]
    public long ProductId { get;  set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.NumberIsRequired)]
    public double UnitPrice { get;  set; }
}