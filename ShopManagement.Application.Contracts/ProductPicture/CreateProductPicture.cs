using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.ProductPicture;

public class CreateProductPicture
{
    [Range(1,100000,ErrorMessage = ValidationMessage.IsRequired)]
    public long ProductId { get;  set; }

    [FileExtensionLimitation(new[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.InvalideFileFormat)]
    [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
    public IFormFile Picture { get;  set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureAlt { get;  set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string PictureTitle { get;  set; }
    public string BackgroundColor { get;  set; }
}