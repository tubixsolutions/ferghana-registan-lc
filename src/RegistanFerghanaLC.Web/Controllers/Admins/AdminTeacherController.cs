using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Services.AdminService;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    [Route("adminteachers")]
    public class AdminTeacherController : Controller
    {
        private readonly IAdminTeacherService _adminTeacherService;
        private readonly int _pageSize = 5;

        public AdminTeacherController(IAdminTeacherService adminTeacherService)
        {
            _adminTeacherService = adminTeacherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            PagedList<TeacherViewDto> teachers;
            if (String.IsNullOrEmpty(search))
            {
                teachers = await _adminTeacherService.GetAllAsync(new PaginationParams(page, _pageSize));
            }
            else
            {
                teachers = await _adminTeacherService.SearchAsync(new PaginationParams(page, _pageSize), search);
            }
            ViewBag.HomeTitle = "Teacher";
            ViewBag.AdminTeacherSearch = search;
            return View("Index", teachers);
        }

        [HttpGet("register")]
        public ViewResult Register() => View("Register");

        [HttpPost("register")]
        public async Task<IActionResult> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminTeacherService.RegisterTeacherAsync(teacherRegisterDto);
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

        [HttpGet("Delete")]
        public async Task<ViewResult> DeleteAsync(int id)
        {
            var teacher = await _adminTeacherService.GetByIdAsync(id);
            if (teacher != null)
            {
                return View("Delete", teacher);
            }
            return View("adminteacher");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTeacherAsync(int Id)
        {
            var res = await _adminTeacherService.DeleteAsync(Id);
            if (res) return RedirectToAction("index", "adminteacher", new { area = "" });
            return View();
        }
        [HttpGet("updateredirect")]
        public async Task<IActionResult> UpdateRedirectAsync(int teacherId)
        {
            var teacher = await _adminTeacherService.GetByIdAsync(teacherId);

            var dto = new TeacherUpdateDto()
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PhoneNumber = teacher.PhoneNumber,
                Image = teacher.ImagePath,
                BirthDate = teacher.BirthDate,
                Subject = teacher.Subject
            };

            ViewBag.HomeTittle = "Admin/Teacher/Update";
            ViewBag.teacherId = teacherId;
            return View("Update", dto);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(int teacherId, TeacherUpdateDto dto)
        {
            var res = await _adminTeacherService.UpdateAsync(dto, teacherId);
            if (res)
            {
                return RedirectToAction("Index", "adminteachers", new { area = "" });
            }
            else return await UpdateRedirectAsync(teacherId);
        }
    }
}
