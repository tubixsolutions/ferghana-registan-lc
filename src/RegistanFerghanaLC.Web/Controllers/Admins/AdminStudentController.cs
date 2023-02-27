using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    public class AdminStudentController : Controller
    {

        private readonly IAdminStudentService _adminStudentService;

        public AdminStudentController(IAdminStudentService adminStudentService)
        {
            _adminStudentService = adminStudentService;
        }

        [HttpGet("register")]
        public ViewResult Register()
        {
            return View();
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
