
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;

namespace RegistanFerghanaLC.Service.Services.AdminService;

public class AdminTeacherService : IAdminTeacherService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;

    public AdminTeacherService(IUnitOfWork unitOfWork, IAuthService authService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
    }
    public async Task<bool> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto)
    {
        var checkTeacher = await _repository.Teachers.FirstOrDefault(x => x.PhoneNumber == teacherRegisterDto.PhoneNumber);
        if (checkTeacher is not null) throw new AlreadyExistingException(nameof(teacherRegisterDto.PhoneNumber), "This number is already registered!");

        var hasherResult = PasswordHasher.Hash(teacherRegisterDto.Password);
        var newTeacher = (Teacher)teacherRegisterDto;
        newTeacher.PasswordHash = hasherResult.Hash;
        newTeacher.Salt = hasherResult.Salt;

        _repository.Teachers.Add(newTeacher);
        var dbResult = await _repository.SaveChangesAsync();
        return dbResult > 0;
    }
}
