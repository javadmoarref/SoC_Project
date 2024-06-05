using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class LogModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<InventoryOperationViewModel> InventoryOperation { get; set; }
        private readonly IInventoryApplication _inventoryApplication;

        public LogModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet(long id)
        {
            InventoryOperation = _inventoryApplication.GetLog(id);
        }

    }
}
