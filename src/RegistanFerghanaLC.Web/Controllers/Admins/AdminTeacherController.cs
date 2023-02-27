using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    public class AdminTeacherController : Controller
    {
        private readonly IAdminTeacherService _adminTeacherService;

        public AdminTeacherController(IAdminTeacherService adminTeacherService)
        {
            _adminTeacherService = adminTeacherService;
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

    }
}
