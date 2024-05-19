using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IncreaseModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public IncreaseInventory Command { get; set; }
        private readonly IInventoryApplication _inventoryApplication;

        public IncreaseModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet(long id)
        {
            Command = new IncreaseInventory()
            {
                InventoryId = id
            };
        }

        public IActionResult OnPost(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = new IncreaseInventory()
            {
                InventoryId =command.InventoryId
            };
            return Page();
        }
    }
}
