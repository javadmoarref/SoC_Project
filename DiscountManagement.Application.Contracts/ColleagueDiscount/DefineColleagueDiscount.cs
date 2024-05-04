using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount;

public class DefineColleagueDiscount
{
    [Range(1,1000000,ErrorMessage = ValidationMessage.IsRequired)]
    public long ProductId { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public int DiscountRate { get; set; }
}