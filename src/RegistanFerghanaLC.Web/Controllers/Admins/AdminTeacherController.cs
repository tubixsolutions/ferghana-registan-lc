using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    [Route("adminteacher")]
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
        public ViewResult Register()
        {
            return View("TeacherRegister");
        }

        [HttpPost("registerteacher")]
        public async Task<ViewResult> RegisterTeacher(TeacherRegisterDto dto)
        {
            if(ModelState.IsValid)
            {
                var res = await _adminTeacherService.RegisterTeacherAsync(dto);
                if (res)
                {
                    return View("loginteacher");
                }
                else
                {
                    return View("TeacherRegister");
                }
            }
            else
            {
                return View("TeacherRegister");
            }
        }

        [HttpPost("registerteacher")]
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
            var teacher = await _adminTeacherService.GetById(teacherid);
            var dto = new TeacherUpdateDto()
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PhoneNumber= teacher.PhoneNumber,
                Image= teacher.Image,
                BirthDate= teacher.BirthDate,
                Subject= teacher.Subject
            };

            ViewBag.HomeTittle = "Admin/Teacher/Update";
            return View("Update", dto);
        }



    }
}
