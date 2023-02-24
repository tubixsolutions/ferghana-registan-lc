using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;

namespace RegistanFerghanaLC.Service.Interfaces.Admins;

public interface IAdminService
{
    public Task<bool> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto);
    public Task<bool> RegisterStudentAsync(StudentRegisterDto dtudentRegisterDto);
}
