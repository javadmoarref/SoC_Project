﻿using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository;

public class ProductPictureRepository:RepositoryBase<long,ProductPicture>, IProductPictureRepository
{
    private readonly ShopContext _context;
    public ProductPictureRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public EditProductPicture GetDetails(long id)
    {
        return _context.ProductPictures.Select(x=>new EditProductPicture()
        {
            Id = x.Id,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            ProductId = x.ProductId,
            BackgroundColor = x.BackgroundColor
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
    {
        var query = _context.ProductPictures
            .Include(x=>x.Product)
            .Select(x => new ProductPictureViewModel()
            {
                Id = x.Id,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi(),
                Product = x.Product.Name,
                IsRemoved = x.IsRemoved,
                ProductId = x.ProductId,
                BackgroundColor = x.BackgroundColor
            });
        if (searchModel.ProductId != 0)
        {
            query = query.Where(x => x.ProductId == searchModel.ProductId);
        }
        return query.OrderByDescending(x=>x.Id).ToList();
    }

    public ProductPicture GetWithProductAndCategory(long id)
    {
        return _context.ProductPictures.Include(x => x.Product)
            .ThenInclude(x => x.Category)
            .FirstOrDefault(x => x.Id == id);
    }
}