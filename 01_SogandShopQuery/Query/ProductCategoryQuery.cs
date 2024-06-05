﻿using _0_Framework.Application;
using _01_SogandShopQuery.Contracts.Product;
using _01_SogandShopQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_SogandShopQuery.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;
    private readonly DiscountContext _discountContext;
    public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
    {
        _shopContext = shopContext;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
    }

    public List<ProductCategoryQueryModel> GetProductCategories()
    {
        return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel()
        {
            Name = x.Name,
            Picture = x.Picture,
            PictureAlt = x.PictureAlt,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug,
            BackgroundColor = x.BackgroundColor
        }).ToList();
    }

    public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
    {
        var inventories = _inventoryContext.Inventory
            .Select(x => new
            {
                x.ProductId,
                x.UnitPrice
            }).ToList();
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new
            {
                x.ProductId,
                x.DiscountRate
            }).ToList();
        var categories =_shopContext.ProductCategories
            .Include(x => x.Products)
            .ThenInclude(x => x.Category)
            .Select(x => new ProductCategoryQueryModel()
            {
                Id=x.Id,
                Name = x.Name,
                Products = MapProducts(x.Products)
            }).ToList();
        foreach (var category in categories)
        {
            foreach (var product in category.Products)
            {
                var productInventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.HasDiscount = product.DiscountRate > 0;
                        var discountAmount =Math.Round((price * product.DiscountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
               
            }
        }

        return categories;
    }

    public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
    {
        var inventory = _inventoryContext.Inventory
            .Select(x => new
            {
                x.ProductId,
                x.InStock,
                x.UnitPrice
            }).ToList();
        var discounts = _discountContext.CustomerDiscounts
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new
            {
                x.ProductId,
                x.DiscountRate
            }).ToList();
        var category = _shopContext.ProductCategories
            .Include(x => x.Products)
            .ThenInclude(x => x.Category)
            .Select(x => new ProductCategoryQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                Keywords = x.Keywords,
                Slug = x.Slug,
                Products = MapProducts(x.Products)
            })
            .FirstOrDefault(x => x.Slug == slug);
        foreach (var product in category.Products)
        {
            var productInventory = inventory
                .FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                product.InStock=productInventory.InStock;
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                var discount = _discountContext.CustomerDiscounts
                    .FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    product.DiscountRate= discount.DiscountRate;
                    product.HasDiscount=product.DiscountRate>0;
                    var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
        }

        category.Products = category.Products.Where(x => x.InStock).ToList();

        return category;
    }

    private static List<ProductQueryModel> MapProducts(List<Product> products)
    {
        return products.Select(product => new ProductQueryModel()
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category.Name,
            Picture = product.Picture,
            PictureAlt = product.PictureAlt,
            PictureTitle = product.PictureTitle,
            Slug = product.Slug,
            BackgroundColor = product.BackgroundColor
        }).ToList();
    }
}