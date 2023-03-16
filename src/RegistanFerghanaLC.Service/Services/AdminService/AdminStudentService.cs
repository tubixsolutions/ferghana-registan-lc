
using AutoMapper;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities;
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
    private readonly IImageService _imageService;
    private readonly IStudentSubjectService _studentSubjectService;

    public AdminStudentService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper, IImageService imageService, IStudentSubjectService studentSubjectService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
        this._mapper = mapper;
        this._imageService = imageService;
        this._studentSubjectService = studentSubjectService;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await  _repository.Students.FindByIdAsync(id);
        if (student is null)
        {
            throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found.");
        }
        if(!String.IsNullOrEmpty(student.Image))
        {
            var imageRes = await _imageService.DeleteImageAsync(student.Image);
        }
        _repository.StudentSubjects.Delete(id);
        _repository.Students.Delete(id);
        var res = await _repository.SaveChangesAsync();
        return res > 0;
    }

    public async Task<PagedList<StudentBaseViewModel>> GetAllAsync(PaginationParams @params)
    {
        /* var query = _repository.Students.GetAll().OrderBy(x => x.CreatedAt).Select(x => _mapper.Map<StudentBaseViewModel>(x));
         var students = await PagedList<StudentBaseViewModel>.ToPagedListAsync(query, @params);
         if (students.Count != 0) return students;
         else throw new StatusCodeException(HttpStatusCode.NotFound, "No student in the database.");
    */
        var query = (from student in _repository.Students.GetAll()
                     let studentSubjects = _repository.StudentSubjects.GetAll()
                     .Where(ss => ss.StudentId == student.Id).ToList()
                     let subjects = (from ss in studentSubjects 
                                     join s in _repository.Subjects.GetAll()
                                     on ss.SubjectId equals s.Id
                                     select s.Name).ToList()
                                    
                     select new StudentBaseViewModel()
                     {
                         Id = student.Id,
                         FirstName = student.FirstName,
                         LastName = student.LastName,
                         PhoneNumber = student.PhoneNumber,
                         WeeklyLimit = student.WeeklyLimit,
                         Image = student.Image,
                         Subjects = subjects,
                         
                     }
                     );
        return await PagedList<StudentBaseViewModel>.ToPagedListAsync(query, @params);

             
    }

    public async Task<StudentViewModel> GetByIdAsync(int id)
    {
        var query = (from student in _repository.Students.GetAll()
                       let studentSubjects = _repository.StudentSubjects.GetAll()
                       .Where(ss => ss.StudentId == student.Id).ToList()
                       let subjects = (from ss in studentSubjects
                                       join s in _repository.Subjects.GetAll()
                                       on ss.SubjectId equals s.Id
                                       select s.Name).ToList()

                       select new StudentViewModel()
                       {
                           Id = student.Id,
                           FirstName = student.FirstName,
                           LastName = student.LastName,
                           PhoneNumber = student.PhoneNumber,
                           WeeklyLimit = student.WeeklyLimit,
                           Image = student.Image,
                           Subjects = subjects,
                           StudentLevel = student.StudentLevel,
                           CreatedAt = student.CreatedAt,
                           BirthDate= student.BirthDate,
                       }
                     ).FirstOrDefault();
        if (query is null)
            throw new StatusCodeException(HttpStatusCode.NotFound, "Student is not found");
        var res = _mapper.Map<StudentViewModel>(query);
        return res;
    }

    public async Task<PagedList<StudentBaseViewModel>> GetByNameAsync(PaginationParams @params, string name)
    {
        var query = _repository.Students.Where( x=> x.FirstName.ToLower().Contains(name.ToLower()) 
        || x.LastName.ToLower().Contains(name.ToLower())).OrderByDescending(x=>x.FirstName).Select(x => _mapper.Map<StudentBaseViewModel>(x));
        var students = await PagedList<StudentBaseViewModel>.ToPagedListAsync(query, @params);
        return students;
    }


    public async Task<List<StudentViewModel>> GetFileAllAsync()
    {
        var query = (from student in _repository.Students.GetAll()
                     let studentSubjects = _repository.StudentSubjects.GetAll()
                     .Where(ss => ss.StudentId == student.Id).ToList()
                     let subjects = (from ss in studentSubjects
                                     join s in _repository.Subjects.GetAll()
                                     on ss.SubjectId equals s.Id
                                     select s.Name).ToList()

                     select new StudentViewModel()
                     {
                         Id = student.Id,
                         FirstName = student.FirstName,
                         LastName = student.LastName,
                         PhoneNumber = student.PhoneNumber,
                         WeeklyLimit = student.WeeklyLimit,
                         Image = student.Image,
                         Subjects = subjects,
                         StudentLevel = student.StudentLevel,
                         BirthDate = student.BirthDate,
                     }
                     );
        return await query.ToListAsync();
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
        string subject = studentRegisterDto.Subject;
        var subRes = await _studentSubjectService.SaveStudentSubjectAsync(newStudent.Id, subject);
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
        student.Image = await _imageService.SaveImageAsync(studentAllUpdateDto.Image);
        _repository.Students.Update(id, student);

        var result = await _repository.SaveChangesAsync();
        return result > 0;
    }
}
