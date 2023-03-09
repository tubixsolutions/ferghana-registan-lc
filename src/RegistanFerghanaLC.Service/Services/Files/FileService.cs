using Microsoft.AspNetCore.Hosting;
using RegistanFerghanaLC.Service.Interfaces.Files;
using RegistanFerghanaLC.Web.Models;

namespace RegistanFerghanaLC.Service.Services.Files
{
    public class FileService : IFileService
    {
        private readonly string _rootPath;
        public FileService(IWebHostEnvironment webHostEnvironment) 
        {
            this._rootPath = webHostEnvironment.WebRootPath;
        }
        public async Task<string> CreateFile(FileViewModels filemodel)
        {
            string path = Path.Combine("files", Guid.NewGuid().ToString() + ".xlsx");
            string fullPath = Path.Combine(_rootPath, path);
            var stream = new FileStream(fullPath, FileMode.Create);
            await filemodel.File.CopyToAsync(stream);
            stream.Close();
            return fullPath;
        }

        public async Task<bool> DeleteFileAsync(string path)
        {
            System.IO.File.Delete(path);
            return 1 > 0;
        }
    }
}
