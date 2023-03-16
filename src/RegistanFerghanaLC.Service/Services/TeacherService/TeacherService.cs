using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Teachers;

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
        if(teacher.Image != null)
        {
            await _imageService.DeleteImageAsync(teacher.Image);
        }
        teacher.Image = await _imageService.SaveImageAsync(path);
        _repository.Teachers.Update(id, teacher);
        int res = await _repository.SaveChangesAsync();
        return res > 0;
    }
}
