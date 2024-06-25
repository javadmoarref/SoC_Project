﻿using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg;

public interface IProductRepository:IRepository<long,Product>
{
    EditProduct GetDetails(long id);
    List<ProductViewModel> Search(ProductSearchModel searchModel);
    List<ProductViewModel> GetProducts();
    Product GetProductWithCategoryBy(long id);
}
