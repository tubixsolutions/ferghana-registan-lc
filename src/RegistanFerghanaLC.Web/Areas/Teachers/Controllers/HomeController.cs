using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Teachers;

namespace RegistanFerghanaLC.Web.Areas.Teachers.Controllers;

[Route("teachers/home")]
public class HomeController : BaseController
{
    private readonly IAdminTeacherService _teacherService;
    private readonly ITeacherService _service;
    private readonly int _pageSize = 5;

    public HomeController(IAdminTeacherService teacherService, ITeacherService service)
    {
        this._teacherService = teacherService;
        this._service = service;
    }

    [HttpGet("teacher/GetAll")]
    public async Task<IActionResult> Index(int page = 1)
        => Ok(await _teacherService.GetAllAsync( new PaginationParams(page, _pageSize)));
    

    [HttpPut("teacher/Update")]
    public async Task<IActionResult> UpdateAsync(int id, [FromForm] TeacherUpdateDto dto)
        => Ok(await _teacherService.UpdateAsync(dto, id));

    [HttpGet ("teacher/Id")]
    public async Task<IActionResult> GetByIdAsync(int Id)
        => Ok(await _teacherService.GetByIdAsync(Id));

    [HttpPost("teacher/Login")]
    public async Task<IActionResult> LoginAsync([FromForm] AccountLoginDto dto)
        => Ok(await _teacherService.LoginAsync(dto));
    
    [HttpPatch("teacher/imageupdate")]
    public async Task<IActionResult> UpdateImageAsync(int id, IFormFile file)
        => Ok(await _service.ImageUpdateAsync(id, file));
    
    [HttpPatch("teacher/deleteimage")]
    public async Task<IActionResult> DeleteImageAsyn(int id)
        => Ok(await _service.ImageDeleteAsync(id));
}
