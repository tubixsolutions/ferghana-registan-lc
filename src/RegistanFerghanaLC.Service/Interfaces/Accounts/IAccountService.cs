using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;

namespace RegistanFerghanaLC.Service.Interfaces.Accounts
{
    public interface IAccountService
    {
        public Task<string> LoginAsync(AccountLoginDto accountLoginDto);
    }
}
