using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreateArticle Command { get; set; }
        public SelectList ArticleCategories { get; set; }
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public CreateModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet()
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
        }

        public IActionResult OnPost(CreateArticle command)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = ApplicationMessage.InvalidModelState;
                return Page();
            }
            var result = _articleApplication.Create(command);
            if (result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage("Index");
            }
            ViewData["Message"] = result.Message;
            return Page();
        }
    }
}
