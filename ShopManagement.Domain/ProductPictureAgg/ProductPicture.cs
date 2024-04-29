using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg;

public class ProductPicture:EntityBase
{
    public long ProductId { get; private set; }
    public string Picture { get; private set; }
    public string PictureAlt { get; private set; }
    public string PictureTitle { get; private set; }
    public bool IsRemoved { get; private set; }
    public string BackgroundColor { get; private set; }
    public Product Product { get; private set; }

    public ProductPicture(long productId, string picture, string pictureAlt, string pictureTitle,
        string backgroundColor)
    {
        ProductId = productId;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        IsRemoved = false;
        BackgroundColor = backgroundColor;
    }

    public void Edit(long productId, string picture, string pictureAlt, string pictureTitle,
        string backgroundColor)
    {
        ProductId = productId;
        Picture = picture;
        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        BackgroundColor = backgroundColor;
    }

    public void Remove()
    {
        IsRemoved=true;
    }

    public void Restore()
    {
        IsRemoved=false;
    }
}