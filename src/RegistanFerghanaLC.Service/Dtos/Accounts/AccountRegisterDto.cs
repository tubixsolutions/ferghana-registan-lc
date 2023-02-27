using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Domain.Entities;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Common.Attributes;
using RegistanFerghanaLC.Service.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Accounts;
public class AccountRegisterDto: AccountLoginDto
{
    [Required(ErrorMessage = "Enter a name!")]
    public string FirstName { get; set; } = String.Empty;

    [Required(ErrorMessage = "Enter a surname!")]
    public string LastName { get; set; } = String.Empty;

    [Required(ErrorMessage = "Select the role of the person.")]

    [ImageFile]
    public IFormFile? Image { get; set; } 
    public DateTime BirthDate { get; set; }

}
