using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
using RegistanFerghanaLC.Service.ViewModels.TeacherViewModels;

namespace RegistanFerghanaLC.Service.Interfaces.Teachers;
public interface ITeacherService
{
    public  Task<bool> ImageUpdateAsync(int id, IFormFile file);
    
    public Task<bool> ImageDeleteAsync(int id);
    
    public Task<List<string>>? GetFreeTimeAsync(int id, string time);  
    public Task<int> GetTeachersCountAsync(string subject, PaginationParams @params);
    public Task<PagedList<TeacherViewDto>> GetTeachersBySubjectAsync(string subject, PaginationParams @params);
    public Task<List<TeacherGroupDto>> GetTeachersGroupAsync();
<<<<<<< HEAD
    public Task<TeacherRankViewModel> GetRankAsync(int id);
=======

>>>>>>> e7de8ad (gruoped by subject)
}
