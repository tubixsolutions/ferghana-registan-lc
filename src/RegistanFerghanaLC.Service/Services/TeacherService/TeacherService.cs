using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Teachers;

namespace RegistanFerghanaLC.Service.Services.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;

    public TeacherService(IUnitOfWork unitOfWork, IAuthService authService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
    }
    public async Task<string> LoginAsync(AccountLoginDto accountLoginDto)
    {
        var teacher = await _repository.Teachers.FirstOrDefault(x => x.PhoneNumber == accountLoginDto.PhoneNumber);
        if (teacher is null) throw new NotFoundException(nameof(accountLoginDto.PhoneNumber), "No teacher is found with this phone number.");

        var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, teacher.Salt, teacher.PasswordHash);
        if (hasherResult)
        {
            string token = _authService.GenerateToken(teacher, "teacher");
            return token;
        }
        else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
    }
}
