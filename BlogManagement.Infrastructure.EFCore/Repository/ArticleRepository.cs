using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository;

public class ArticleRepository:RepositoryBase<long,Article>,IArticleRepository
{
    private readonly BlogContext _context;

    public ArticleRepository(BlogContext context) : base(context)
    {
        _context = context;
    }

    public EditArticle GetDetails(long id)
    {
        return _context.Articles.Select(x => new EditArticle()
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                PublishDate = x.PublishDate.ToFarsi(),
                CanonicalAddress = x.CanonicalAddress,
                Slug = x.Slug,
                CategoryId = x.CategoryId
            })
            .FirstOrDefault(x => x.Id == id);
    }

    public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
    {
        var query = _context.Articles
            .Include(x=>x.Category)
            .Select(x => new ArticleViewModel()
        {
            Id = x.Id,
            Title = x.Title,
            Picture = x.Picture,
            PublishDate = x.PublishDate.ToFarsi(),
            ShortDescription = x.ShortDescription.Substring(0,Math.Min(x.ShortDescription.Length,50)) + " ...",
            CategoryId = x.CategoryId,
            Category = x.Category.Name
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Title))
        {
            query = query.Where(x => x.Title.Contains(searchModel.Title));
        }

        if (searchModel.CategoryId > 0)
        {
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);
        }

        return query.OrderByDescending(x => x.Id).ToList();
    }

    public Article GetWithCategory(long id)
    {
        return _context.Articles.Include(x => x.Category)
            .FirstOrDefault(x => x.Id == id);
    }
}