﻿using _0_Framework.Application;
using _01_SogandShopQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_SogandShopQuery.Query;

public class ProductQuery:IProductQuery
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;

    public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
    {
        _shopContext = shopContext;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
    }

    public ProductQueryModel GetProductBy(string slug)
    {
        var product= _shopContext.Products
            .Include(x=>x.Comments)
            .Include(x=>x.Category)
            .Select(x=>new ProductQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                BackgroundColor = x.BackgroundColor,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                Category = x.Category.Name,
                Slug = x.Slug,
                CategorySlug = x.Category.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Comments = MapComments(x.Comments)
            }).FirstOrDefault(x => x.Slug == slug);
        if (product == null)
        {
            return new ProductQueryModel();
        }
        var inventory = _inventoryContext.Inventory
            .FirstOrDefault(x => x.ProductId == product.Id);
        if (inventory != null)
        {
            var price = inventory.UnitPrice;
            product.Price=price.ToMoney();
            var discount = _discountContext.CustomerDiscounts
                .FirstOrDefault(x => x.ProductId == product.Id);
            if (discount != null)
            {
                product.DiscountRate = discount.DiscountRate;
                product.HasDiscount = product.DiscountRate > 0;
                var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                product.PriceWithDiscount=(price-discountAmount).ToMoney();
            }
        }

        return product;
    }

    private static List<CommentQueryModel> MapComments(List<Comment> comments)
    {
        return comments
            .Where(x => !x.IsCanceled)
            .Where(x => x.IsConfirmed)
            .Select(x => new CommentQueryModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                Message = x.Message
            }).OrderByDescending(x=>x.Id).ToList();
    }

    public List<ProductQueryModel> GetLatestArrivals()
    {
        var inventories = _inventoryContext.Inventory
            .Select(x => new
            {
                x.ProductId,
                x.UnitPrice,
                x.InStock
            }).ToList();
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new
            {
                x.ProductId,
                x.DiscountRate
            }).ToList();
        var products = _shopContext.Products
            .Include(x => x.Category)
            .Select(product => new ProductQueryModel()
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                CategorySlug = product.Category.Slug,
                ShortDescription = product.ShortDescription,
                BackgroundColor = product.BackgroundColor
            }).OrderByDescending(x=>x.Id).ToList();

        foreach (var product in products)
        {
            var productInventory = inventories
                .FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                product.InStock = productInventory.InStock;
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    product.DiscountRate = discount.DiscountRate;
                    product.HasDiscount = product.DiscountRate > 0;
                    var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }

        return products.Where(x=>x.InStock).Take(6).ToList();
    }

    public List<ProductQueryModel> Search(string value)
    {
        var inventories = _inventoryContext.Inventory
            .Select(x => new
            {
                x.ProductId,
                x.UnitPrice,
                x.InStock
            }).ToList();
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new
            {
                x.ProductId,
                x.DiscountRate
            }).ToList();
        var query = _shopContext.Products
            .Include(x => x.Category)
            .Select(product => new ProductQueryModel()
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                CategorySlug = product.Category.Slug,
                ShortDescription = product.ShortDescription,
                BackgroundColor = product.BackgroundColor
            }).OrderByDescending(x => x.Id).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(value))
        {
            query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
        }

        var products = query.ToList();
        foreach (var product in products)
        {
            var productInventory = inventories
                .FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                product.InStock = productInventory.InStock;
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    product.DiscountRate = discount.DiscountRate;
                    product.HasDiscount = product.DiscountRate > 0;
                    var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }

        return products.Where(x => x.InStock).ToList();
    }
}