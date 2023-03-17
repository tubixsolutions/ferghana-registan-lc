using Microsoft.Net.Http.Headers;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Teachers;

namespace RegistanFerghanaLC.Service.Interfaces.Admins;
public interface IAdminTeacherService
{
    public Task<bool> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto);
    public Task<bool> RegisterAsync(TeacherRegisterDto teacherRegisterDto);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> UpdateAsync(TeacherUpdateDto teacherRegisterDto, int id);
    public Task<TeacherViewDto> GetByIdAsync(int id);
    public Task<PagedList<TeacherViewDto>> GetAllAsync(PaginationParams @params);
    public Task<List<TeacherViewDto>> GetFileAllAsync();
    public Task<string> LoginAsync(AccountLoginDto dto);
    public Task<PagedList<TeacherViewDto>> SearchAsync(PaginationParams @params, String name);

}
