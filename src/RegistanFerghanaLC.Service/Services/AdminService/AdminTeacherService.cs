
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc.Formatters;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
using System.Net;

namespace RegistanFerghanaLC.Service.Services.AdminService;

public class AdminTeacherService : IAdminTeacherService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AdminTeacherService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper)
    {
        this._repository = unitOfWork;
        this._authService = authService;
        _mapper = mapper;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var temp = _repository.Teachers.FindByIdAsync(id);
        if(temp is null)
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Teacher not found");
        _repository.Teachers.Delete(id);
        var result = await _repository.SaveChangesAsync();
        return result > 0;

    }

    public async Task<PagedList<TeacherViewDto>> GetAllAsync(PaginationParams @params)
    {
        var query = _repository.Teachers.GetAll().OrderByDescending(x => x.CreatedAt).Select(x => _mapper.Map<TeacherViewDto>(x));
        //new TeacherViewDto
        //{
        //    id= x.Id,
        //    FirstName= x.FirstName,
        //    LastName = x.LastName,
        //    BirthDate= x.BirthDate,
        //    PartOfDay= x.PartOfDay,
        //    Subject= x.Subject,
        //    PhoneNumber= x.PhoneNumber,
        //    TeacherLevel= x.TeacherLevel,

        //});
        return await PagedList<TeacherViewDto>.ToPagedListAsync(query, @params);
    }

    public async Task<TeacherViewDto> GetByIdAsync(int id)
    {
        var temp = await _repository.Teachers.FindByIdAsync(id);
        if (temp is null)
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Teacher is not Found");
        var res = _mapper.Map<TeacherViewDto>(temp);
        return res;
    }

    public async Task<string> LoginAsync(AccountLoginDto dto)
    {
        var teacher = await _repository.Teachers.FirstOrDefault(x => x.PhoneNumber== dto.PhoneNumber);
        if(teacher is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "teacher is not found");
        }
        var passwordhasher = PasswordHasher.Verify(dto.Password, teacher.Salt, teacher.PasswordHash);
        if (passwordhasher)
        {
            string token = _authService.GenerateToken(teacher, "Teacher");
            return token;
        }
        else
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Incorrect Password");
        }

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

    public async Task<PagedList<TeacherViewDto>> SearchAsync(PaginationParams @params, string name)
    {
        var query = _repository.Teachers.GetAll().Where(x => x.FirstName.ToLower().StartsWith(name.ToLower()) || x.LastName.ToLower().StartsWith(name.ToLower())).OrderByDescending(x => x.CreatedAt).Select(x => _mapper.Map<TeacherViewDto>(x));
        return await PagedList<TeacherViewDto>.ToPagedListAsync(query, @params);

    }

    public async Task<bool> UpdateAsync(TeacherUpdateDto dto, int id)
    {
        var temp = await _repository.Teachers.FindByIdAsync(id);
        if (temp is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");
        _repository.Teachers.TrackingDeteched(temp);
        temp.FirstName = dto.FirstName;
        temp.LastName =  dto.LastName;
        temp.PhoneNumber = dto.PhoneNumber;
        temp.BirthDate = dto.BirthDate;
        temp.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
        _repository.Teachers.Update(id, temp);

        var result = await _repository.SaveChangesAsync();
        return result > 0;
    }

    
}
