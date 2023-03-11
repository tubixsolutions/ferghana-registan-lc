using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Students;
using RegistanFerghanaLC.Service.Services.Common;
using RegistanFerghanaLC.Service.ViewModels.TeacherViewModels;

namespace RegistanFerghanaLC.Service.Services.StudentService;
public class StudentService : IStudentService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;
    private readonly IImageService _imageService;
    public StudentService(IUnitOfWork unitOfWork, IAuthService authService, IImageService imageService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
        _imageService = imageService;
    }

    public Task<PagedList<TeacherBySubjectViewModel>> GetAllTeacherBySubjectAsync(string subject, PaginationParams @params)
    {
        var query = from teacher in _repository.Teachers.GetAll().Where(x => x.Subject == subject)
                    select new TeacherBySubjectViewModel()
                    {
                        Id = teacher.Id,
                        FirstName = teacher.FirstName,
                        LastName = teacher.LastName,
                        TeacherLevel = teacher.TeacherLevel,
                        WorkDays = teacher.WorkDays,
                        ImagePath = teacher.Image
                    };
        return PagedList<TeacherBySubjectViewModel>.ToPagedListAsync(query, @params);
    }
    public async Task<bool> ImageUpdateAsync(int id, IFormFile path)
    {
        var student = await _repository.Students.FindByIdAsync(id);
        if (student == null)
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "teacher is not found");
        _repository.Students.TrackingDeteched(student);
        if (student.Image != null)
        {
            await _imageService.DeleteImageAsync(student.Image);
        }
        student.Image = await _imageService.SaveImageAsync(path);
        _repository.Students.Update(id, student);
        int res = await _repository.SaveChangesAsync();
        return res > 0;

    }
}
