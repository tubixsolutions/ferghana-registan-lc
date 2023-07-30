using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
using RegistanFerghanaLC.Service.ViewModels.TeacherViewModels;

namespace RegistanFerghanaLC.Service.Interfaces.Students;

public interface IStudentService
{
    public Task<PagedList<TeacherBySubjectViewModel>> GetAllTeacherBySubjectAsync(string subject, PaginationParams @params);

    public Task<bool> ImageUpdateAsync(int id, IFormFile file);

    public Task<int> GetLimitStudentAsync(int id);

    public Task<bool> DeleteImageAsync(int id);

    public Task<string> LoginAsync(AccountLoginDto accountLoginDto);

    public Task<StudentViewModel> GetByIdAsync(int id);

    public Task<StudentViewModel> GetByTokenAsync();
}
