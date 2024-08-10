using _0_Framework.Application;
using _01_SogandShopQuery.Contracts.Article;
using _01_SogandShopQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_SogandShopQuery.Query;

public class ArticleCategoryQuery : IArticleCategoryQuery
{
    private readonly BlogContext _context;

    public ArticleCategoryQuery(BlogContext context)
    {
        _context = context;
    }


    public List<ArticleCategoryQueryModel> GetArticleCategories()
    {
        return _context.ArticleCategories
            .Include(x => x.Articles)
            .Select(x => new ArticleCategoryQueryModel()
            {
                Name = x.Name,
                Slug = x.Slug,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ArticlesCount = x.Articles.Count
            })
            .ToList();
    }

    public ArticleCategoryQueryModel GetArticleCategoryWithArticles(string slug)
    {
        var articleCategory = _context.ArticleCategories
            .Include(x => x.Articles)
            .ThenInclude(x=>x.Category)
            .Select(x => new ArticleCategoryQueryModel()
            {
                Articles = MapArticles(x.Articles),
                Slug = slug,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Slug == slug);
        if (articleCategory == null)
        {
            return new ArticleCategoryQueryModel();
        }
        var keywordList=new List<string>();
        foreach (var article in articleCategory.Articles)
        {
            foreach (var item in article.KeywordList)
            {
                keywordList.Add(item);
            }
        }
        articleCategory.KeywordList= keywordList;
        
        return articleCategory;
    }

  

    private static List<ArticleQueryModel> MapArticles(List<Article> articles)
    {
        return articles.Select(x => new ArticleQueryModel()
        {
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Keywords = x.Keywords,
            PublishDate = x.PublishDate.ToFarsi(),
            Title = x.Title,
            Slug = x.Slug,
            CategoryName = x.Category.Name,
            CategorySlug = x.Category.Slug,
            KeywordList = x.Keywords.Split(" ").ToList()
        }).ToList();
    }
}

   
