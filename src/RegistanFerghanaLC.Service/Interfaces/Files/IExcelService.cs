using RegistanFerghanaLC.Service.Dtos.FileViewModels;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using System.IO;

namespace RegistanFerghanaLC.Service.Interfaces.Files
{
    public interface IExcelService
    {
        public Task<List<StudentRegisterDto>> ReadStudentFileAsync(FileModeldto filemodel);
        public Task<List<TeacherRegisterDto>> ReadTeacherFileAsync(FileModeldto filemodel);
    }
}
