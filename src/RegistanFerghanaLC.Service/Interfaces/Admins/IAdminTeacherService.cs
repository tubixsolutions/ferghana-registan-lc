using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Teachers;

namespace RegistanFerghanaLC.Service.Interfaces.Admins;
public interface IAdminTeacherService
{
    public Task<bool> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> UpdateAsync(TeacherRegisterDto teacherRegisterDto, long id);
    public Task<TeacherViewDto> GetById(int id);
    public Task<IEnumerable<TeacherViewDto>> GetAll(PaginationParams @params);

}
