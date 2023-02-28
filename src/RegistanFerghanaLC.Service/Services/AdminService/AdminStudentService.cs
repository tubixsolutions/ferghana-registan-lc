
using AutoMapper;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Services.Common;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
using System.Net;

namespace RegistanFerghanaLC.Service.Services.AdminService;

public class AdminStudentService : IAdminStudentService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AdminStudentService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper)
    {
        this._repository = unitOfWork;
        this._authService = authService;
        this._mapper = mapper;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student =  _repository.Students.FirstOrDefault(x => x.Id == id);
        if (student is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found.");
        }
        _repository.Students.Delete(id);
        var res = await _repository.SaveChangesAsync();
        return res > 0;
    }

    public async Task<PagedList<StudentBaseViewModel>> GetAllAsync(PaginationParams @params)
    {
        var query = _repository.Students.GetAll().OrderBy(x => x.CreatedAt).Select(x => _mapper.Map<StudentBaseViewModel>(x));
        return await PagedList<StudentBaseViewModel>.ToPagedListAsync(query, @params);
    }

    public async Task<StudentViewModel> GetByIdAsync(int id)
    {
        var student = await _repository.Students.FindByIdAsync(id);
        if (student is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");

        var res = _mapper.Map<StudentViewModel>(student);
        return res;
    }

    public async Task<StudentViewModel> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
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

    public async Task<bool> UpdateAsync(int id, StudentRegisterDto studentRegisterDto)
    {
        var student = await _repository.Students.FindByIdAsync(id);
        if (student is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");
        

        student.FirstName = studentRegisterDto.FirstName;
        student.LastName = studentRegisterDto.LastName;
        student.PhoneNumber = studentRegisterDto.PhoneNumber;
        student.BirthDate = studentRegisterDto.BirthDate;
        student.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
        _repository.Students.Update(id, student);

        var result = await _repository.SaveChangesAsync();
        return result > 0;
    }
}
