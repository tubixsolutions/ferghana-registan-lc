using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Teachers;

namespace RegistanFerghanaLC.Service.Interfaces.Admins;
public interface IAdminTeacherService
{
    public Task<bool> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> UpdateAsync(TeacherUpdateDto teacherRegisterDto, int id);
    public Task<TeacherViewDto> GetByIdAsync(int id);

    public Task<PagedList<TeacherViewDto>> GetAllAsync(PaginationParams @params);

}
