using RegistanFerghanaLC.Web.Models;

namespace RegistanFerghanaLC.Service.Interfaces.Files
{
    public interface IFileService
    {
        public Task<string> CreateFile(FileViewModels filemodel);
        public Task<bool> DeleteFileAsync(string path);
    }
}
