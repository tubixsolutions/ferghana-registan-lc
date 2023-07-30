using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Admins;

namespace RegistanFerghanaLC.Service.Interfaces.Accounts
{
    public interface IAccountService
    {
        public Task<bool> AdminRegisterAsync(AdminRegisterDto adminRegisterDto);
        public Task<string> LoginAsync(AccountLoginDto accountLoginDto);
    }
}
