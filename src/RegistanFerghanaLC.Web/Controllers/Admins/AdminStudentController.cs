using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RegistanFerghanaLC.Domain.Entities.Students;
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
    private readonly int _pageSize = 5;

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
    public async Task<IActionResult> Index(string search, int page = 1)
    {
        PagedList<StudentBaseViewModel> students;
        if(String.IsNullOrEmpty(search))
        {
            students = await _adminStudentService.GetAllAsync(new PaginationParams(page, _pageSize));
        }
        else
        {
            students = await _adminStudentService.GetByNameAsync(new PaginationParams(page, _pageSize), search);
        }
        ViewBag.HomeTitle = "Student";
        ViewBag.AdminStudentSearch = search;
        return View("Index", students);
    }

    [HttpGet("delete")]
    public async Task<ViewResult> Delete(int id)
    {
        var student = await _adminStudentService.GetByIdAsync(id);
        if (student != null)
        {
            return View("Delete", student);
        }
        return View();
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        var res = await _adminStudentService.DeleteAsync(Id);
        return RedirectToAction("Index");
    }

    [HttpGet("update")]
    public async Task<ViewResult> Update(int id)
    {
        var student = await _adminStudentService.GetByIdAsync(id);
        if (student != null) { return View("Update", student); }
        else return View("Index");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(int id, StudentAllUpdateDto dto)
    {
        var res = await _adminStudentService.UpdateAsync(id, dto);
        if (res) return RedirectToAction("Index");
        return View("Error");
       
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _adminStudentService.GetByIdAsync(id);
        if (student is not null) return View("GetById", student);
        return View("Index");
    }

}
