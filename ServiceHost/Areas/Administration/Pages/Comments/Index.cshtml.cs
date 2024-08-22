using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public SelectList Products { get; set; }
        public CommentSearchModel SearchModel { get; set; }
        private readonly ICommentApplication _commentApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(ICommentApplication commentApplication, IProductApplication productApplication)
        {
            _commentApplication = commentApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
            Products =new SelectList(_productApplication.GetProducts(),"Id","Name");
        }

        public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirm(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            var result = _commentApplication.Cancel(id);
            Message=result.Message;
            return RedirectToPage("./Index");
        }

    }
}
