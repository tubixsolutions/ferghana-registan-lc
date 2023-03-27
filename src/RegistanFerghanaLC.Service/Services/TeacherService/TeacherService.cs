using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Teachers;
using RegistanFerghanaLC.Service.ViewModels.SalaryViewModels;
using RegistanFerghanaLC.Service.ViewModels.TeacherViewModels;

namespace RegistanFerghanaLC.Service.Services.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;
    private readonly IImageService _imageService;

    public TeacherService(IUnitOfWork unitOfWork, IAuthService authService, IImageService imageService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
        this._imageService = imageService;
    }

    public Task<List<string>>? GetFreeTimeAsync(int id, string time)
    {
        var query = _repository.ExtraLessons.GetAll().Where(x => x.TeacherId == id && x.StartTime >=
                    DateTime.Parse(time)
                    && x.EndTime < DateTime.Parse(time).AddDays(1)).Select(x => x.StartTime.
                    ToString("dd-MM-yyyy HH:mm")).ToListAsync();
        return query;
    }

    public async Task<bool> ImageDeleteAsync(int id)
    {
        var teacher = await _repository.Teachers.FindByIdAsync(id);
        _repository.Teachers.TrackingDeteched(teacher!);
        await _imageService.DeleteImageAsync(teacher!.Image!);
        teacher.Image = "";
        _repository.Teachers.Update(id, teacher);
        var res = await _repository.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> ImageUpdateAsync(int id, IFormFile path)
    {
        var teacher = await _repository.Teachers.FindByIdAsync(id);
        if (teacher == null)
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "teacher is not found");
        _repository.Teachers.TrackingDeteched(teacher);
        if (teacher.Image != null)
        {
            await _imageService.DeleteImageAsync(teacher.Image);
        }
        teacher.Image = await _imageService.SaveImageAsync(path);
        _repository.Teachers.Update(id, teacher);
        int res = await _repository.SaveChangesAsync();
        return res > 0;
    }
    public async Task<int> GetTeachersCountAsync(string subject, PaginationParams @params)
    {
        var query = _repository.Teachers.GetAll().Where(x => x.Subject.ToLower() == subject.ToLower()).
            OrderByDescending(x => x.Id).Select(x => (TeacherViewDto)x).ToList();
        if (query is null)
            throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "teachers not found");
        return query.Count;
    }
    public async Task<PagedList<TeacherViewDto>> GetTeachersBySubjectAsync(string subject, PaginationParams @params)
    {
        var query = _repository.Teachers.GetAll().Where(x => x.Subject.ToLower() == subject.ToLower()).
            OrderByDescending(x => x.Id).Select(x => (TeacherViewDto)x);
        return await PagedList<TeacherViewDto>.ToPagedListAsync(query, @params);
    }
    public async  Task<List<TeacherGroupDto>>  GetTeachersGroupAsync()
    {
        var res =  await _repository.Teachers.GetAll().GroupBy(x => x.Subject)
            .Select(res => new TeacherGroupDto()
            {
                Major = res.First().Subject,
                Count = res.Count()
            }).ToListAsync();
        return res;
    }
    public async Task<TeacherRankViewModel> GetRankAsync(int id)
    {
        var all = (from extra in _repository.ExtraLessons.GetAll().Where(x => x.TeacherId == id)
                   join detail in _repository.ExtraLessonDetails.GetAll()
                   on extra.Id equals detail.ExtraLessonId
                   select new
                   {
                       rank = detail.Rank,
                   }).ToListAsync().Result;

        TeacherRankViewModel model = new TeacherRankViewModel()
        {
            Id = id,
            AverageRank = all.Average(x => x.rank),
            LessonsNumber = all.Count(),
            One = all.Count(x => x.rank == 1),
            Two = all.Count(x => x.rank == 2),
            Three = all.Count(x => x.rank == 3),
            Four = all.Count(x => x.rank == 4),
            Five = all.Count(x => x.rank == 5),
        };
        return model;
    }
}
