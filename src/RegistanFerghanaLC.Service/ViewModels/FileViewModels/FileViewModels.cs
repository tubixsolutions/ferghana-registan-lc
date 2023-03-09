using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Service.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Web.Models
{
    public class FileViewModels
    {
        [AllowedFiles(new string[] { ".xlsx" })]
        [Required]
        public IFormFile File { get; set; }
    }
}
