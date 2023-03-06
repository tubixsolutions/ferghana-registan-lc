
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        var students = await PagedList<StudentBaseViewModel>.ToPagedListAsync(query, @params);
        if (students.Count != 0) return students;
        else throw new StatusCodeException(HttpStatusCode.NotFound, "No student in the database.");
    }

    public async Task<StudentViewModel> GetByIdAsync(int id)
    {
        var student = await _repository.Students.FindByIdAsync(id);
        if (student is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");

        var res = _mapper.Map<StudentViewModel>(student);
        return res;
    }

    public async Task<PagedList<StudentBaseViewModel>> GetByNameAsync(PaginationParams @params, string name)
    {
        var query = _repository.Students.Where( x=> x.FirstName.ToLower().Contains(name.ToLower()) 
        || x.LastName.ToLower().Contains(name.ToLower())).Select(x => _mapper.Map<StudentBaseViewModel>(x));
        var students = await PagedList<StudentBaseViewModel>.ToPagedListAsync(query, @params);
        if (students.Count != 0) return students;
        else throw new StatusCodeException(HttpStatusCode.NotFound, "No info has been according to the input.");
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

    public async Task<bool> UpdateAsync(int id, StudentAllUpdateDto studentAllUpdateDto)
    {
        var student = await _repository.Students.FindByIdAsync(id);
        if (student is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");
        _repository.Students.TrackingDeteched(student);
        student.FirstName = studentAllUpdateDto.FirstName;
        student.LastName = studentAllUpdateDto.LastName;
        student.PhoneNumber = studentAllUpdateDto.PhoneNumber;
        student.BirthDate = studentAllUpdateDto.BirthDate;
        student.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
        _repository.Students.Update(id, student);

        var result = await _repository.SaveChangesAsync();
        return result > 0;
    }
}
