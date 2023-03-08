using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;

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
        public async Task<IActionResult> Index(int page = 1)
        {
            var teachers = await _adminTeacherService.GetAllAsync(new PaginationParams(page, _pageSize));
            ViewBag.HomeTitle = "Teacher";
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

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteTeacherAsync(int Id)
        {
            var result = await _adminTeacherService.DeleteAsync(Id);
            if (result)
            {
                return RedirectToAction("Index", "Teachers", new { area = "" });
            }
            else
                return RedirectToAction("Index", "Teachers", new { area = "" });

        }
        [HttpGet("Update")]
        public async Task<IActionResult> UpdateRedirectAsync(int teacherid)
        {
            var teacher = await _adminTeacherService.GetByIdAsync(teacherid);

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
            return View("Update", dto);
        }
    }
}
