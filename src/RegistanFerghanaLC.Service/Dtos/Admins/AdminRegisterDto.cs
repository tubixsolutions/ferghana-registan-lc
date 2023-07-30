
using RegistanFerghanaLC.Service.Dtos.Accounts;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Admins;
public class AdminRegisterDto : AccountRegisterDto
{
    [Required(ErrorMessage = "Please enter the address of the admin.")]
    public string Address { get; set; } = string.Empty;
}
