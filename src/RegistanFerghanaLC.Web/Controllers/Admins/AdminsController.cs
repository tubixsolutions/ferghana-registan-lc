using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    public class AdminsController : Controller
    {
        private readonly IAdminTeacherService _adminTeacherService;
        private readonly IAdminStudentService _adminStudentService;

        public AdminsController(IAdminTeacherService adminTeacherService, IAdminStudentService adminStudentService)
        {
            _adminTeacherService = adminTeacherService;
            _adminStudentService = adminStudentService;
        }

        [HttpGet("register")]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost("register/teacher")]
        public async Task<IActionResult> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminTeacherService.RegisterTeacherAsync(teacherRegisterDto);
                if (result)
                {
                    return RedirectToAction("login", "accounts", new { area = "" });
                }
                else
                {
                    return Register();
                }
            }
            else return Register();
        }

        [HttpPost("register/student")]
        public async Task<IActionResult> RegisterStudentAsync(StudentRegisterDto studentRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminStudentService.RegisterStudentAsync(studentRegisterDto);
                if (result)
                {
                    return RedirectToAction("login", "accounts", new { area = "" });
                }
                else
                {
                    return Register();
                }
            }
            else return Register();
        }
    }
}
