using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class ReduceModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ReduceInventory Command { get; set; }
        private readonly IInventoryApplication _inventoryApplication;

        public ReduceModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet(long id)
        {
            Command = new ReduceInventory()
            {
                InventoryId = id
            };
        }

        public IActionResult OnPost(ReduceInventory command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result = _inventoryApplication.Reduce(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = new ReduceInventory()
            {
                InventoryId =command.InventoryId
            };
            return Page();
        }
    }
}
