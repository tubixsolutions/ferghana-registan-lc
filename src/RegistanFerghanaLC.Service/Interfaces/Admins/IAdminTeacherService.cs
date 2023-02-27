using RegistanFerghanaLC.Service.Dtos.Teachers;

namespace RegistanFerghanaLC.Service.Interfaces.Admins;
public interface IAdminTeacherService
{
    public Task<bool> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto);

}
