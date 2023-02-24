using RegistanFerghanaLC.Service.Dtos.Accounts;

namespace RegistanFerghanaLC.Service.Interfaces.Students;

public interface IStudentService
{
    public Task<string> LoginAsync(AccountLoginDto accountLoginDto);

}
