using _0_Framework.Application;
using _0_Framework.Infrastructure;
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
    private readonly IAuthHelper _authHelper;
    public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, IAuthHelper authHelper)
    {
        _shopContext = shopContext;
        _inventoryContext = inventoryContext;
        _discountContext = discountContext;
        _authHelper = authHelper;
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
        var customerDiscounts = _discountContext.CustomerDiscounts
          .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
          .Select(x => new
          {
              x.ProductId,
              x.DiscountRate
          }).ToList();
        var colleagueDiscounts = _discountContext.ColleagueDiscounts
            .Where(x => x.IsRemoved == false)
            .Select(x => new
            {
                x.ProductId,
                x.DiscountRate
            });
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
                    //var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    //if (discount != null)
                    //{
                    //    product.DiscountRate = discount.DiscountRate;
                    //    product.HasDiscount = product.DiscountRate > 0;
                    //    var discountAmount =Math.Round((price * product.DiscountRate) / 100);
                    //    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    //}
                    var currentAccountRole = _authHelper.CurrentAccountRole();
                    if (currentAccountRole == Roles.ColleagueUser)
                    {
                        var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (colleagueDiscount != null)
                        {
                            product.DiscountRate = colleagueDiscount.DiscountRate;
                            product.HasDiscount = product.DiscountRate > 0;
                            var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }
                    else
                    {

                        var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (customerDiscount != null)
                        {
                            product.DiscountRate = customerDiscount.DiscountRate;
                            product.HasDiscount = product.DiscountRate > 0;
                            var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }

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
        var customerDiscounts = _discountContext.CustomerDiscounts
           .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
           .Select(x => new
           {
               x.ProductId,
               x.DiscountRate
           }).ToList();
        var colleagueDiscounts = _discountContext.ColleagueDiscounts
            .Where(x => x.IsRemoved == false)
            .Select(x => new
            {
                x.ProductId,
                x.DiscountRate
            });
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
            }).FirstOrDefault(x => x.Slug == slug);
        if (category == null)
        {
            return new ProductCategoryQueryModel();
        }
        foreach (var product in category.Products)
        {
            var productInventory = inventory
                .FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                product.InStock=productInventory.InStock;
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                //var discount = _discountContext.CustomerDiscounts
                //    .FirstOrDefault(x => x.ProductId == product.Id);
                //if (discount != null)
                //{
                //    product.DiscountRate= discount.DiscountRate;
                //    product.HasDiscount=product.DiscountRate>0;
                //    var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                //    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                //}
                var currentAccountRole = _authHelper.CurrentAccountRole();
                if (currentAccountRole == Roles.ColleagueUser)
                {
                    var colleagueDiscount =colleagueDiscounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (colleagueDiscount != null)
                    {
                        product.DiscountRate = colleagueDiscount.DiscountRate;
                        product.HasDiscount = product.DiscountRate > 0;
                        var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
                else
                {

                    var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (customerDiscount != null)
                    {
                        product.DiscountRate = customerDiscount.DiscountRate;
                        product.HasDiscount = product.DiscountRate > 0;
                        var discountAmount = Math.Round((price * product.DiscountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }

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