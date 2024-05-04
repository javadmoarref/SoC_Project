using System.Linq.Expressions;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class ProductCategoryRepository:RepositoryBase<long,ProductCategory>, IProductCategoryRepository
{
    private readonly ShopContext _context;

    public ProductCategoryRepository(ShopContext context):base(context)
    {
        _context = context;
    }

    public EditProductCategory GetDetails(long id)
    {
        return _context.ProductCategories.Select(x=>new EditProductCategory()
        {
            Id = x.Id,
            Name = x.Name,
            Picture = x.Picture,
            Description = x.Description,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            MetaDescription = x.MetaDescription,
            Keywords = x.Keywords,
            Slug = x.Slug,
            BackgroundColor = x.BackgroundColor
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
    {
        var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Picture = x.Picture,
            CreationDate = x.CreationDate.ToFarsi(),
            BackgroundColor = x.BackgroundColor
        });
        if (!string.IsNullOrWhiteSpace(searchModel.Name))
        {
            query = query.Where(x => x.Name.Contains(searchModel.Name));
        }
        return query.OrderByDescending(x=>x.Id).ToList();
    }

    public List<ProductCategoryViewModel> GetProductCategories()
    {
        return _context.ProductCategories.Select(x => new ProductCategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}