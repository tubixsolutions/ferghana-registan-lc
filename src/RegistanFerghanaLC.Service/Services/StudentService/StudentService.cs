using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Students;
using RegistanFerghanaLC.Service.ViewModels.TeacherViewModels;

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

    public Task<PagedList<TeacherBySubjectViewModel>> GetAllTeacherBySubjectAsync(string subject, PaginationParams @params)
    {
        var query = from teacher in _repository.Teachers.GetAll().Where(x => x.Subject.ToLower() == subject.ToLower())
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

    public Task<int> GetLimitStudentAsync(int id)
    {
        DateTime date;
        var day = DateTime.Now.DayOfWeek;
        if (day == DayOfWeek.Friday) date = DateTime.Now.Date.AddDays(-4);
        else if (day == DayOfWeek.Monday) date = DateTime.Now.Date;
        else if (day == DayOfWeek.Tuesday) date = DateTime.Now.Date.AddDays(-1);
        else if (day == DayOfWeek.Wednesday) date = DateTime.Now.Date.AddDays(-2);
        else if (day == DayOfWeek.Thursday) date = DateTime.Now.Date.AddDays(-3);
        else if (day == DayOfWeek.Saturday) date = DateTime.Now.Date.AddDays(-5);
        else date = DateTime.Now.Date.AddDays(-6);

        var limit = _repository.ExtraLessons.GetAll().Where(x => x.CreatedAt > date).CountAsync();

        return limit;
    }
}
