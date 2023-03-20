using RegistanFerghanaLC.Domain.Common;
using RegistanFerghanaLC.Service.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Accounts;
public class AccountLoginDto : BaseEntity
{
    [Required(ErrorMessage = "Enter a phone number!")]
    [PhoneNumber]
    public string PhoneNumber { get; set; } = String.Empty;

    [Required(ErrorMessage = "Enter a password!")]
    [StrongPassword]
    public string Password { get; set; } = String.Empty;
}
