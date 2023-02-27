using RegistanFerghanaLC.Service.Dtos.Students;

namespace RegistanFerghanaLC.Service.Interfaces.Admins;
public interface IAdminStudentService
{
    public Task<bool> RegisterStudentAsync(StudentRegisterDto dtudentRegisterDto);

}
