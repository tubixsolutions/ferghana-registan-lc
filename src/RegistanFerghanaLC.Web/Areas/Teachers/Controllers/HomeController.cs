using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Areas.Teachers.Controllers;

[Route("teachers/home")]
public class HomeController : BaseController
{
    private readonly IAdminTeacherService _teacherService;
    private readonly int _pageSize = 5;

    public HomeController(IAdminTeacherService teacherService)
    {
        this._teacherService = teacherService;
    }

    [HttpGet("teacher/GetAll")]
    public async Task<IActionResult> Index(int page = 1)
    {
        return Ok(await _teacherService.GetAllAsync( new PaginationParams(page, _pageSize)));
    }

    [HttpPut("teacher/Update")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] TeacherUpdateDto dto)
    {
        return Ok(await _teacherService.UpdateAsync(dto, id));
    }

    [HttpGet ("teacher/Id")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        return Ok(await _teacherService.GetByIdAsync(Id));
    }

    [HttpPost("teacher/Login")]
    public async Task<IActionResult> LoginAsync([FromForm] AccountLoginDto dto)
    {
        return Ok(await _teacherService.LoginAsync(dto));
    }
}
