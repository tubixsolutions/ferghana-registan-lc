using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Dtos.FileViewModels;
using RegistanFerghanaLC.Service.Interfaces.Files;

namespace RegistanFerghanaLC.Service.Services.Files
{
    public class FileService : IFileService
    {
        private readonly string ASSETS_FOLDER;
        private readonly string MEDIA_FOLDER;
        private readonly string RESOUCE_IMAGE_FOLDER;
        private readonly string AVATAR_FOLDER;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            ASSETS_FOLDER = webHostEnvironment.WebRootPath;
            MEDIA_FOLDER = "media";
            RESOUCE_IMAGE_FOLDER = Path.Combine(MEDIA_FOLDER, "images");
            AVATAR_FOLDER = Path.Combine(MEDIA_FOLDER, "avatars");
        }

        public async Task<string> CreateFile(FileModeldto filemodel)
        {
            string path = Path.Combine("files", Guid.NewGuid().ToString() + ".xlsx");
            string fullPath = Path.Combine(ASSETS_FOLDER, path);
            var stream = new FileStream(fullPath, FileMode.Create);
            await filemodel.File.CopyToAsync(stream);
            stream.Close();
            return fullPath;
        }

        public async Task<bool> DeleteFileAsync(string path)
        {
            File.Delete(path);
            return 1 > 0;
        }

        public async Task<bool> DeleteImageAsync(string imagePartPath)
        {
            string path = Path.Combine(ASSETS_FOLDER, imagePartPath);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else return false;
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            string fileName = ImageHelper.UniqueName(image.FileName);
            string partPath = Path.Combine(RESOUCE_IMAGE_FOLDER, fileName);
            string path = Path.Combine(ASSETS_FOLDER, partPath);
            var stream = new FileStream(path, FileMode.Create);
            await image.CopyToAsync(stream);
            stream.Close();
            return partPath;
        }
    }
}
