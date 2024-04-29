using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductPicture;

public class CreateProductPicture
{
    [Range(1,100000,ErrorMessage = ValidationMessage.IsRequired)]
    public long ProductId { get;  set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Picture { get;  set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureAlt { get;  set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureTitle { get;  set; }
    public string BackgroundColor { get;  set; }
}