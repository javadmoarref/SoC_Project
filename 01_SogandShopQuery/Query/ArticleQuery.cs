using _0_Framework.Application;
using _01_SogandShopQuery.Contracts.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_SogandShopQuery.Query;

public class ArticleQuery:IArticleQuery
{
    private readonly BlogContext _context;

    public ArticleQuery(BlogContext context)
    {
        _context = context;
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
        return article;
    }

}