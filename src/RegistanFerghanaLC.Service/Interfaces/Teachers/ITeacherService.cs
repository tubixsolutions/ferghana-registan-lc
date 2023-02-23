using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Teachers;

namespace RegistanFerghanaLC.Service.Interfaces.Teachers;
public interface ITeacherService
{
    public Task<string> LoginAsync(AccountLoginDto accountLoginDto);

}
