using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Entities.Teachers;
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
    private readonly IAdminSubjectService _subjectService;
    private readonly IMapper _mapper;
    private readonly int _pageSize = 5;

    public AdminStudentController(IAdminStudentService adminStudentService, IAdminSubjectService subjectService, IMapper mapper)
    {
        _adminStudentService = adminStudentService;
        _subjectService = subjectService;
        _mapper = mapper;
    }

    [HttpGet("register")]
    public ViewResult Register()
    {
        ViewBag.Subjects = _subjectService.GetAllAsync();
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
        var dto = new StudentAllUpdateDto()
        {
            FirstName = student.FirstName,
            LastName = student.LastName,
            PhoneNumber= student.PhoneNumber,
            BirthDate = student.BirthDate,
            StudentLevel = student.StudentLevel,

        };
        if (student != null) 
        {
            ViewBag.studentId = id;
            return View("Update", dto); 
        }
        else return View("Index");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateAsync(int id, StudentAllUpdateDto dto)
    {
        var res = await _adminStudentService.UpdateAsync(id, dto);
        if (res)
        {
            return RedirectToAction("Index", "adminstudent", new { area = "" });
        }
        else return await Update(id);


    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _adminStudentService.GetByIdAsync(id);
        if (student is not null) return View("GetById", student);
        return View("Index");
    }

}
