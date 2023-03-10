using RegistanFerghanaLC.Service.Dtos.FileViewModels;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using System.IO;

namespace RegistanFerghanaLC.Service.Interfaces.Files
{
    public interface IExcelService
    {
        public Task<List<TeacherRegisterDto>> ReadExcelFileAsync(FileModeldto filemodel);
    }
}
