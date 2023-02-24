using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;

namespace RegistanFerghanaLC.Service.Services.AdminService;
public class AdminService : IAdminService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;

    public AdminService(IUnitOfWork unitOfWork, IAuthService authService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
    }
    public async Task<bool> RegisterStudentAsync(StudentRegisterDto studentRegisterDto)
    {
        var checkStudent = await _repository.Students.FirstOrDefault(x => x.PhoneNumber == studentRegisterDto.PhoneNumber);
        if (checkStudent is not null) throw new AlreadyExistingException(nameof(studentRegisterDto.PhoneNumber), "This number is already registered!");

        var hasherResult = PasswordHasher.Hash(studentRegisterDto.Password);
        var newStudent = (Student)studentRegisterDto;
        newStudent.PasswordHash = hasherResult.Hash;
        newStudent.Salt = hasherResult.Salt;

        _repository.Students.Add(newStudent);
        var dbResult = await _repository.SaveChangesAsync();
        return dbResult > 0;
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
