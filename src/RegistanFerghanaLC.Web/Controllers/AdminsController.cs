using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Controllers
{
    public class AdminsController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            this._adminService = adminService;
        }

        [HttpGet("register")]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost("register/teacher")]
        public async Task<IActionResult> RegisterTeacherAsync(TeacherRegisterDto teacherRegisterDto)
        {
            if(ModelState.IsValid)
            {
                var result = await _adminService.RegisterTeacherAsync(teacherRegisterDto);
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
                var result = await _adminService.RegisterStudentAsync(studentRegisterDto);
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
