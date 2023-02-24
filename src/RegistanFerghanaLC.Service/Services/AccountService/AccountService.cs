using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Service.Common.Exceptions;
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
    public async Task<string> LoginAsync(AccountLoginDto accountLoginDto)
    {
        var admin = await _repository.Admins.FirstOrDefault(x => x.PhoneNumber == accountLoginDto.PhoneNumber);
        if (admin is null)
        {
            var teacher = await _repository.Teachers.FirstOrDefault(x=>x.PhoneNumber == accountLoginDto.PhoneNumber);
            if(teacher is null)
            {
                var student = await _repository.Students.FirstOrDefault(x => x.PhoneNumber == accountLoginDto.PhoneNumber);
                if (student is null) throw new NotFoundException(nameof(accountLoginDto.PhoneNumber), "No user with this phone number is found!");
                else
                {
                    var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, student.Salt, student.PasswordHash);
                    if (hasherResult)
                    {
                        string token = _authService.GenerateToken(student, "student");
                        return token;
                    }
                    else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
                }
            }
            else
            {
                var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, teacher.Salt, teacher.PasswordHash);
                if (hasherResult)
                {
                    string token = _authService.GenerateToken(teacher, "teacher");
                    return token;
                }
                else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
            }
            
        }
        else
        {
            var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, admin.Salt, admin.PasswordHash);
            if (hasherResult)
            {
                string token = _authService.GenerateToken(admin, "admin");
                return token;
            }
            else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
        }
    
    }

    
}
