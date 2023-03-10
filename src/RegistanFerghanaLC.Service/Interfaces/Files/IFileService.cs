using RegistanFerghanaLC.Service.Dtos.FileViewModels;

namespace RegistanFerghanaLC.Service.Interfaces.Files
{
    public interface IFileService
    {
        public Task<string> CreateFile(FileModeldto filemodel);
        public Task<bool> DeleteFileAsync(string path);
    }
}
