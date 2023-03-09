using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Web.Models;
using System.IO;

namespace RegistanFerghanaLC.Service.Interfaces.Files
{
    public interface IExcelService
    {
        public Task<List<TeacherRegisterDto>> ReadExcelFileAsync(FileViewModels filemodel);
    }
}
