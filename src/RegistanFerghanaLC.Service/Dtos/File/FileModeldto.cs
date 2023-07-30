using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Service.Common.Attributes;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.FileViewModels
{
    public class FileModeldto
    {
        [AllowedFiles(new string[] { ".xlsx" })]
        [Required]
        public IFormFile? File { get; set; }
        public PagedList<TeacherViewDto> Teachers { get; set; }
        public PagedList<StudentBaseViewModel> Students { get; set; }
        public List<StudentViewModel> StudentsUnsaved { get; set; }
        public List<TeacherViewDto> TeachersUnsaved { get; set; }
    }
}
