﻿using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount;

public class DefineColleagueDiscount
{
    [Range(1,1000000,ErrorMessage = ValidationMessage.IsRequired)]
    public long ProductId { get; set; }

    [Range(1,99,ErrorMessage = ValidationMessage.NumberIsRequired)]
    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public int DiscountRate { get; set; }
}