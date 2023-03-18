
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
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
using RegistanFerghanaLC.Service.Interfaces.Files;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
using System.Net;

namespace RegistanFerghanaLC.Service.Services.AdminService;

public class AdminTeacherService : IAdminTeacherService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AdminTeacherService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper, IFileService fileService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
        this._mapper = mapper;
        this._fileService = fileService;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var temp = await _repository.Teachers.FindByIdAsync(id);
        if(temp is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Teacher not found");
        _repository.Teachers.Delete(id);
        var result = await _repository.SaveChangesAsync();
        return result > 0;

    }

    public async Task<bool> DeleteImageAsync(int teacherId)
    {
        var teacher = await _repository.Teachers.FindByIdAsync(teacherId);
        if (teacher is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Teacher not found");
        else
        {
            await _fileService.DeleteImageAsync(teacher.Image!);
            teacher.Image = "";
            _repository.Teachers.Update(teacherId, teacher);
            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }

    public async Task<PagedList<TeacherViewDto>> GetAllAsync(PaginationParams @params)
    {
        var query = _repository.Teachers.GetAll().OrderByDescending(x => x.CreatedAt).Select(x => _mapper.Map<TeacherViewDto>(x));
        return await PagedList<TeacherViewDto>.ToPagedListAsync(query, @params);
    }

    public async Task<TeacherViewDto> GetByIdAsync(int id)
    {
        var temp = await _repository.Teachers.FindByIdAsync(id);
        if (temp is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Teacher is not Found");
        var res = _mapper.Map<TeacherViewDto>(temp);
        return res;
    }

    public async Task<TeacherViewDto> GetByPhoneNumberAsync(string phoneNumber)
    {
        var teacher = await _repository.Teachers.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        if (teacher is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Teacher is not found");
        var teacherView = _mapper.Map < TeacherViewDto >(teacher);
        return teacherView;
    }

    public async Task<List<TeacherViewDto>> GetFileAllAsync()
    {
        var query = _repository.Teachers.GetAll().OrderByDescending(x => x.CreatedAt).Select(x => _mapper.Map<TeacherViewDto>(x));
        return await query.ToListAsync();
    }

    public async Task<string> LoginAsync(AccountLoginDto dto)
    {
        var teacher = await _repository.Teachers.FirstOrDefault(x => x.PhoneNumber == dto.PhoneNumber);
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

    public async Task<bool> RegisterAsync(TeacherRegisterDto teacherRegisterDto)
    {
        var checkTeacher = await _repository.Teachers.FirstOrDefault(x => x.PhoneNumber == teacherRegisterDto.PhoneNumber);
        if (checkTeacher is not null) return false;

        var hasherResult = PasswordHasher.Hash(teacherRegisterDto.Password);
        var newTeacher = (Teacher)teacherRegisterDto;
        newTeacher.PasswordHash = hasherResult.Hash;
        newTeacher.Salt = hasherResult.Salt;

         _repository.Teachers.Add(newTeacher);
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

    public async Task<PagedList<TeacherViewDto>> SearchAsync(PaginationParams @params, string name)
    {
        var query = _repository.Teachers.GetAll().Where(x => x.FirstName.ToLower().StartsWith(name.ToLower()) || x.LastName.ToLower().StartsWith(name.ToLower())).OrderByDescending(x => x.CreatedAt).Select(x => _mapper.Map<TeacherViewDto>(x));
        return await PagedList<TeacherViewDto>.ToPagedListAsync(query, @params);
    }

    public async Task<bool> UpdateAsync(TeacherUpdateDto dto, int id)
    {
        var temp = await _repository.Teachers.FindByIdAsync(id);
        if (temp is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Teachers is not found");
        else
        {
            _repository.Teachers.TrackingDeteched(temp);
            if(dto is not null)
            {
                temp.FirstName = String.IsNullOrEmpty(dto.FirstName) ? temp.FirstName : dto.FirstName;
                temp.LastName = String.IsNullOrEmpty(dto.LastName) ? temp.LastName : dto.LastName;
                temp.Image = String.IsNullOrEmpty(dto.ImagePath) ? temp.Image : dto.ImagePath;
                temp.WorkDays = dto.WorkDays;
                temp.PhoneNumber = String.IsNullOrEmpty(dto.PhoneNumber) ? temp.PhoneNumber : dto.PhoneNumber;
                temp.TeacherLevel = String.IsNullOrEmpty(dto.TeacherLevel) ? temp.TeacherLevel : dto.TeacherLevel;
                temp.BirthDate = dto.BirthDate;
                temp.Subject = String.IsNullOrEmpty(dto.Subject) ? temp.Subject : dto.Subject;
                temp.PartOfDay = dto.PartOfDay;
            }
            
            temp.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
            _repository.Teachers.Update(id, temp);

            var result = await _repository.SaveChangesAsync();
            return result > 0;
        }
    }
}
