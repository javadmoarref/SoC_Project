using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application;

public class ProductPictureApplication:IProductPictureApplication
{
    private readonly IProductPictureRepository _productPictureRepository;
    private readonly IProductRepository _productRepository;
    private readonly IFileUploader _fileUploader;

    public ProductPictureApplication(IProductPictureRepository productPictureRepository,
        IProductRepository productRepository, IFileUploader fileUploader)
    {
        _productPictureRepository = productPictureRepository;
        _productRepository = productRepository;
        _fileUploader = fileUploader;
    }

    public OperationResult Create(CreateProductPicture command)
    {
        var operation=new OperationResult();
        var product = _productRepository.GetProductWithCategoryBy(command.ProductId);
        var path = Path.Combine(product.Category.Slug,product.Slug);
        var fileName = _fileUploader.Upload(command.Picture, path);
        if (_productPictureRepository.Exists(x => x.Picture == fileName &&
                                                  x.ProductId == command.ProductId))
        {
            return operation.Failed(ApplicationMessage.DuplicatedRecord);
        }
        var productPicture = new ProductPicture(command.ProductId, fileName, command.PictureAlt,
            command.PictureTitle,command.BackgroundColor);
        _productPictureRepository.Create(productPicture);
        _productPictureRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditProductPicture command)
    {
        var operation= new OperationResult();
        var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);
        if (productPicture == null)
        {
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }
        var path = Path.Combine(productPicture.Product.Category.Slug, productPicture.Product.Slug);
        var fileName = _fileUploader.Upload(command.Picture,path);
        if (_productPictureRepository.Exists(x => x.Picture == fileName &&
                                                  x.ProductId == command.ProductId && x.Id != command.Id))
        {
            return operation.Failed(ApplicationMessage.DuplicatedRecord);
        }
        productPicture.Edit(command.ProductId,fileName,command.PictureAlt,command.PictureTitle,
            command.BackgroundColor);
        _productPictureRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Remove(long id)
    {
        var operation=new OperationResult();
        var productPicture= _productPictureRepository.Get(id);
        if (productPicture == null)
        {
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }
        productPicture.Remove();
        _productPictureRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var productPicture = _productPictureRepository.Get(id);
        if (productPicture == null)
        {
            return operation.Failed(ApplicationMessage.RecordNotFound);
        }
        productPicture.Restore();
        _productPictureRepository.SaveChanges();
        return operation.Succedded();
    }

    public EditProductPicture GetDetails(long id)
    {
        return _productPictureRepository.GetDetails(id);
    }

    public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
    {
        return _productPictureRepository.Search(searchModel);
    }
}