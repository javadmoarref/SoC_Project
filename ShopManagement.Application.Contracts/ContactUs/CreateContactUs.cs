using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ContactUS
{
    public class CreateContactUs
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string InstagramAccount { get; set; }
    }
}
