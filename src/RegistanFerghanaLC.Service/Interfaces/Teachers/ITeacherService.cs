using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Teachers;

namespace RegistanFerghanaLC.Service.Interfaces.Teachers;
public interface ITeacherService
{
    public  Task<bool> ImageUpdateAsync(int id, IFormFile file);
    public Task<bool> ImageDeleteAsync(int id);
}
