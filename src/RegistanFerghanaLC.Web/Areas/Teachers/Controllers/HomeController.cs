using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.ExtraLesson;
using RegistanFerghanaLC.Service.Interfaces.Teachers;

namespace RegistanFerghanaLC.Web.Areas.Teachers.Controllers;

[Area("teachers/home")]
public class HomeController : BaseController
{
    private readonly IAdminTeacherService _teacherService;
    private readonly IExtraLessonService _extraLessonService;
    private readonly ITeacherService _service;
    private readonly int _pageSize = 5;

    public HomeController(IAdminTeacherService teacherService, ITeacherService service, IExtraLessonService extraLesson)
    {
        this._extraLessonService = extraLesson;
        this._teacherService = teacherService;
        this._service = service;
    }

    [HttpGet("teacher/GetAll")]
    public async Task<IActionResult> Index(int page = 1)
        => Ok(await _teacherService.GetAllAsync(new PaginationParams(page, _pageSize)));

    [HttpGet("teacher/get-teacher-rank")]
    public async Task<IActionResult> GetRankAsync(int id)
        => Ok(await _service.GetRankAsync(id));

    [HttpPut("teacher/update")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] TeacherUpdateDto dto)
        => Ok(await _teacherService.UpdateAsync(dto, id));


    [HttpGet("teacher/get-by-id")]
    public async Task<IActionResult> GetByIdAsync(int Id)
        => Ok(await _teacherService.GetByIdAsync(Id));


    [HttpPost("teacher/login")]
    public async Task<IActionResult> LoginAsync([FromForm] AccountLoginDto dto)
        => Ok(await _teacherService.LoginAsync(dto));


    [HttpPatch("teacher/update-image")]
    public async Task<IActionResult> UpdateImageAsync(int id, IFormFile file)
        => Ok(await _service.ImageUpdateAsync(id, file));


    [HttpPatch("teacher/delete-image")]
    public async Task<IActionResult> DeleteImageAsyn(int id)
        => Ok(await _service.ImageDeleteAsync(id));


    [HttpGet("teacher/extra-lessons")]
    public async Task<IActionResult> GetAllExtraLessonAsync(int teacherId, int page = 1)
    {
        return Ok(await _extraLessonService.GetAllByDateAsync(teacherId, new PaginationParams(page, _pageSize)));
    }
    [HttpGet("teacher/count")]
    public async Task<IActionResult> GetTeachersCountAsync(string subject, int page = 1)
    {
        return Ok(await _service.GetTeachersCountAsync(subject, new PaginationParams(page, _pageSize)));
    }
    [HttpGet("teacher/get-by-subject")]
    public async Task<IActionResult> GetTeachersBySubjectAsync(string subject, int page = 1)
    {
        return Ok(await _service.GetTeachersBySubjectAsync(subject, new PaginationParams(page, _pageSize)));
    }
}
