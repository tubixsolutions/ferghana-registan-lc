using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Common;

namespace RegistanFerghanaLC.Service.Services.AccountService;
public class AccountService : IAccountService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;

    public AccountService(IUnitOfWork unitOfWork, IAuthService authService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
    }
    /*public Task<string> LoginAsync(AccountLoginDto accountLoginDto)
    {
        var emailedUser = await _repository.Users.FirstOrDefaultAsync(x => x.Email == accountLoginDto.Email);
        if (emailedUser is null) throw new ModelErrorException(nameof(accountLoginDto.Email), "Bunday email bilan foydalanuvchi mavjud emas!");

        var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, emailedUser.Salt, emailedUser.PasswordHash);
        if (hasherResult)
        {
            string token = _authService.GenerateToken(emailedUser);
            return token;
        }
        else throw new ModelErrorException(nameof(accountLoginDto.Password), "Parol xato terildi!");
    }*/

    public Task<bool> RegisterStudentAsync(StudentRegisterDto dtudentRegisterDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto)
    {
        throw new NotImplementedException();
    }
}
