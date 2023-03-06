using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;

namespace RegistanFerghanaLC.Web.Controllers.Admins;

[Route("adminstudents")]
public class AdminStudentController : Controller
{

    private readonly IAdminStudentService _adminStudentService;
    private readonly int _pageSize = 6;

    public AdminStudentController(IAdminStudentService adminStudentService)
    {
        _adminStudentService = adminStudentService;
    }

    [HttpGet("register")]
    public ViewResult Register()
    {
        return View("Register");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudentAsync(StudentRegisterDto studentRegisterDto)
    {
        if (ModelState.IsValid)
        {
            var result = await _adminStudentService.RegisterStudentAsync(studentRegisterDto);
            if (result)
            {
                return RedirectToAction("index", "home", new { area = "" });
            }
            else
            {
                return Register();
            }
        }
        else return Register();
    }
    [HttpGet]
    public async Task<IActionResult> Index(int page = 1)
    {
        var students = await _adminStudentService.GetAllAsync(new PaginationParams(page, _pageSize));
        ViewBag.HomeTitle = "Students";
        return View("Index", students);
    }

    [HttpGet("delete")]
    public async Task<ViewResult> Delete(int id)
    {
        var student = await _adminStudentService.GetByIdAsync(id);
        if(student != null)
        {
            return View(student);
        }
        return View();
    }
    [HttpPost("delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var res = await _adminStudentService.DeleteAsync(id);
        if (res) return RedirectToAction("index", "home", new { area = "" });
        return View();
    }
    
}
