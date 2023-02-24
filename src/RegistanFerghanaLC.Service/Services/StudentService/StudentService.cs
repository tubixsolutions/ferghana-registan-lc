using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Students;

namespace RegistanFerghanaLC.Service.Services.StudentService;
public class StudentService : IStudentService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;

    public StudentService(IUnitOfWork unitOfWork, IAuthService authService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
    }
    public async Task<string> LoginAsync(AccountLoginDto accountLoginDto)
    {
        var student = await _repository.Students.FirstOrDefault(x => x.PhoneNumber == accountLoginDto.PhoneNumber);
        if (student is null) throw new NotFoundException(nameof(accountLoginDto.PhoneNumber), "No student is found with this phone number.");

        var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, student.Salt, student.PasswordHash);
        if (hasherResult)
        {
            string token = _authService.GenerateToken(student, "student");
            return token;
        }
        else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
    }
}
