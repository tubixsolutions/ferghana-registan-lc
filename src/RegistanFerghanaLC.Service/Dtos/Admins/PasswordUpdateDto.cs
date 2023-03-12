using RegistanFerghanaLC.Service.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Dtos.Admins
{
    public class PasswordUpdateDto
    {

        [Required(ErrorMessage = "Enter a password!")]
        [StrongPassword]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a password!")]
        [StrongPassword]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a password!")]
        [StrongPassword]
        public string VerifyPassword { get; set; } = string.Empty;
    }
}
