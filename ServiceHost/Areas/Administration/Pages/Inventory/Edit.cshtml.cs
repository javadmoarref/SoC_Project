using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditInventory Command { get; set; }
        public SelectList Products { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public EditModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet(long id)
        {
            Command = _inventoryApplication.GetDetails(id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnPost(EditInventory command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result = _inventoryApplication.Edit(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            Command = _inventoryApplication.GetDetails(command.Id);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            return Page();
        }
    }
}
