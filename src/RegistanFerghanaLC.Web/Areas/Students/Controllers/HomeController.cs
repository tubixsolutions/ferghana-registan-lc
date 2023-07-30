using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.ExtraLesson;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.ExtraLesson;
using RegistanFerghanaLC.Service.Interfaces.Students;
using RegistanFerghanaLC.Service.Interfaces.Teachers;

namespace RegistanFerghanaLC.Web.Areas.Students.Controllers;

public class HomeController : BaseController
{
    private readonly int _pageSize = 5;
    private readonly IAdminStudentService _adminstudentService;
    private readonly IStudentService _studentService;
    private readonly IExtraLessonService _extraLessonService;
    private readonly ITeacherService _teacherSerivice;
    private readonly IExtraLessonDetailsService _extraLessonDetails;
    public HomeController(IAdminStudentService adminStudentService, IStudentService studentService, IExtraLessonService extraLessonService, ITeacherService _teacherSerivice, IExtraLessonDetailsService extraLessonDetails)
    {
        this._adminstudentService = adminStudentService;
        this._studentService = studentService;
        this._extraLessonService = extraLessonService;
        this._teacherSerivice = _teacherSerivice;
        this._extraLessonDetails = extraLessonDetails;
    }

    [HttpPost("student/login")]
    public async Task<IActionResult> LoginAsync([FromForm] AccountLoginDto accountLoginDto)
        => Ok(await _studentService.LoginAsync(accountLoginDto));

    [HttpGet("student/get-by-id")]
    public async Task<IActionResult> GetByIdAsync(int id)
        => Ok(await _studentService.GetByIdAsync(id));
    [Authorize]
    [HttpGet("student/get-by-token")]
    public async Task<IActionResult> GetByTokenAsync()
        => Ok(await _studentService.GetByTokenAsync());

    [HttpGet("student/GetAll")]
    public async Task<IActionResult> Index(int page = 1)
        => Ok(await _adminstudentService.GetAllAsync(new PaginationParams(page, _pageSize)));


    [HttpGet("student/subject")]
    public async Task<IActionResult> GetTeachersBySubjectAsync(string subject, int page = 1)
        => Ok(await _studentService.GetAllTeacherBySubjectAsync(subject, new PaginationParams(page, _pageSize)));


    [HttpPost("student/extra-lesson")]
    public async Task<IActionResult> CreateExtraLessonAsync([FromForm] ExtraLessonCreateDto createDto)
        => Ok(await _extraLessonService.CreateAsync(createDto));


    [HttpPut("student/extra-lesson-details-update")]
    public async Task<IActionResult> UpdateExtraLessonAsync(int id, ExtraLessonDetailsUpdateDto updateDto)
        => Ok(await _extraLessonDetails.UpdateAsync(id, updateDto));


    [HttpPut("student/update")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] StudentAllUpdateDto dto)
        => Ok(await _adminstudentService.UpdateAsync(id, dto));


    [HttpPatch("student/update-image")]
    public async Task<IActionResult> UpdateImageAsync(int id, IFormFile file)
        => Ok(await _studentService.ImageUpdateAsync(id, file));


    [HttpGet("student/limit")]
    public async Task<IActionResult> GetLimitStudentAsync(int id)
        => Ok(await _studentService.GetLimitStudentAsync(id));


    [HttpPatch("student/delete-image")]
    public async Task<IActionResult> DeleteImageAsync(int id)
        => Ok(await _studentService.DeleteImageAsync(id));


    [HttpGet("get-free-time")]
    public async Task<IActionResult> GetFreeTimeAsync(int id, string time)
        => Ok(await _teacherSerivice.GetFreeTimeAsync(id, time));
    [HttpGet("get-group-teachers")]
    public async Task<IActionResult> GetGroupedAsync(int page = 1)
        => Ok(await _teacherSerivice.GetTeachersGroupAsync());
}
