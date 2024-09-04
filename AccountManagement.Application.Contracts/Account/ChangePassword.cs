using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account;

public class ChangePassword
{
    public long Id { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    public string Password { get; set; }

    [Required(ErrorMessage = ValidationMessage.IsRequired)]
    [Compare(nameof(Password), ErrorMessage = ValidationMessage.PasswordNotMatch)]
    public string RePassword { get; set; }
}