using _0_Framework.Application;
using _01_SogandShopQuery.Contracts.Article;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore.Requirements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article { get; set; }
        private readonly IArticleQuery _articleQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(ICommentApplication commentApplication, IArticleQuery articleQuery)
        {
            _commentApplication = commentApplication;
            _articleQuery = articleQuery;
        }

        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var result = _commentApplication.Add(command);
            if (result.IsSuccedded)
            {
                ViewData["Message"] = ApplicationMessage.MessageSendSuccess;
            }
            else
            {
                ViewData["Message"] = ApplicationMessage.MessageSendWrong;
            }
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
