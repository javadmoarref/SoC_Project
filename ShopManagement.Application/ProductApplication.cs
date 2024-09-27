﻿using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application;

public class ProductApplication:IProductApplication
{
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IFileUploader _fileUploader;

    public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
    {
        _productRepository = productRepository;
        _fileUploader = fileUploader;
        _productCategoryRepository = productCategoryRepository;
    }

    public OperationResult Create(CreateProduct command)
    {
        var operation=new OperationResult();
        if (_productRepository.Exists(x => x.Name == command.Name))
        {
            return operation.Failed(ApplicationMessage.DuplicatedRecord);
        }
        var slug=command.Slug.Slugify();
        var categorySlug = _productCategoryRepository.GetCategorySlugBy(command.CategoryId);
        //var picturePath = Path.Combine(categorySlug, slug);
        var picturePath = $"{categorySlug}/{slug}";
        var fileName = _fileUploader.Upload(command.Picture, picturePath);
        var product = new Product(command.Name, command.Code,  command.ShortDescription,
            command.Description, fileName, command.PictureAlt, command.PictureTitle, slug, command.Keywords,
            command.MetaDescription, command.CategoryId,command.BackgroundColor);
        _productRepository.Create(product);
        _productRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditProduct command)
    {
        var operation= new OperationResult();
        var product = _productRepository.GetProductWithCategoryBy(command.Id);
        if (product == null)
        {
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }

        if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
        {
            return operation.Failed(ApplicationMessage.DuplicatedRecord);
        }
        var slug=command.Slug.Slugify();
        var categorySlug = product.Category.Slug;
        //var picturePath = Path.Combine(categorySlug, slug);
        var picturePath = $"{categorySlug}/{slug}";
        var fileName = _fileUploader.Upload(command.Picture, picturePath);
        product.Edit(command.Name,command.Code,command.ShortDescription,command.Description,
            fileName,command.PictureAlt,command.PictureTitle,slug,command.Keywords,
            command.MetaDescription,command.CategoryId,command.BackgroundColor);
        _productRepository.SaveChanges();
        return operation.Succedded();
    }


    public EditProduct GetDetails(long id)
    {
        return _productRepository.GetDetails(id);
    }

    public List<ProductViewModel> Search(ProductSearchModel searchModel)
    {
        return _productRepository.Search(searchModel);
    }

    public List<ProductViewModel> GetProducts()
    {
        return _productRepository.GetProducts();
    }
}