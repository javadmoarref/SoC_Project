using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class ProductRepository:RepositoryBase<long,Product>,IProductRepository
{
    private readonly ShopContext _context;
    public ProductRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public EditProduct GetDetails(long id)
    {
        return _context.Products.Select(x => new EditProduct()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Keywords = x.Keywords,
            Code = x.Code,
            MetaDescription = x.MetaDescription,
            ShortDescription = x.ShortDescription,
            Slug = x.Slug,
            CategoryId = x.CategoryId,
            BackgroundColor = x.BackgroundColor
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<ProductViewModel> Search(ProductSearchModel searchModel)
    {
        var query = _context.Products
            .Include(x=>x.Category)
            .Select(x => new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            CreationDate = x.CreationDate.ToFarsi(),
            Picture = x.Picture,
            Category = x.Category.Name,
            CategoryId = x.CategoryId,
            Code = x.Code,
            BackgroundColor = x.BackgroundColor
        });
        if (!string.IsNullOrWhiteSpace(searchModel.Name))
        {
            query = query.Where(x => x.Name.Contains(searchModel.Name));
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Code))
        {
            query = query.Where(x => x.Code.Contains(searchModel.Code));
        }

        if (searchModel.CategoryId != 0)
        {
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);
        }

        return query.OrderByDescending(x => x.Id).ToList();
    }

    public List<ProductViewModel> GetProducts()
    {
        return _context.Products.Select(x => new ProductViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public Product GetProductWithCategoryBy(long id)
    {
        return _context.Products.Include(x => x.Category)
            .FirstOrDefault(x => x.Id == id);
    }
}