using _0_Framework.Application;
using _01_SogandShopQuery.Contracts.Article;
using _01_SogandShopQuery.Contracts.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Requirements;
using Microsoft.EntityFrameworkCore;

namespace _01_SogandShopQuery.Query;

public class ArticleQuery:IArticleQuery
{
    private readonly BlogContext _context;
    private readonly CommentContext _commentContext;

    public ArticleQuery(BlogContext context, CommentContext commentContext)
    {
        _context = context;
        _commentContext = commentContext;
    }

    public List<ArticleQueryModel> LatestArticles()
    {
        return _context.Articles.Include(x => x.Category)
            .Where(x => x.PublishDate <= DateTime.Now)
            .Select(x => new ArticleQueryModel()
            {
                CategoryName = x.Category.Name,
                CategoryId = x.Category.Id,
                CategorySlug = x.Category.Slug,
                PublishDate = x.PublishDate.ToFarsi(),
                Title = x.Title,
                Description = x.Description,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription
            }).Take(3).ToList();
    }

    public ArticleQueryModel GetArticleDetails(string slug)
    {
        var article= _context.Articles.Include(x => x.Category)
            .Where(x => x.PublishDate <= DateTime.Now)
            .Select(x => new ArticleQueryModel()
            {
                Id = x.Id,
                CategoryName = x.Category.Name,
                CategoryId = x.Category.Id,
                CategorySlug = x.Category.Slug,
                PublishDate = x.PublishDate.ToFarsi(),
                Title = x.Title,
                Description = x.Description,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription
            }).FirstOrDefault(x=>x.Slug==slug);
        if (article == null)
        {
            return new ArticleQueryModel();
        }

        var comments = _commentContext.Comments
            .Where(x => x.Type == CommentType.Article)
            .Where(x => x.OwnerRecordId == article.Id)
            .Where(x => !x.IsCanceled)
            .Where(x => x.IsConfirmed)
            .Select(x => new CommentQueryModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                Message = x.Message,
                ParentId = x.ParentId,
                CreationDate = x.CreationDate.ToFarsi(),
            }).OrderByDescending(x => x.Id).ToList();
        foreach (var comment in comments)
        {
            if (comment.ParentId > 0)
            {
                comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.FullName;
                //comment.ParentCommentDate = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.CreationDate;
                //comment.ParentCommentMessage = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Message;
            }
        }
        article.Comments= comments;
        return article;
    }

}