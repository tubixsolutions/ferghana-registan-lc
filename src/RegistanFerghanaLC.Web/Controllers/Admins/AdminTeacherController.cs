using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    [Route("adminteachers")]
    public class AdminTeacherController : Controller
    {
        private readonly IAdminTeacherService _adminTeacherService;
        private readonly string _rootPath;
        private readonly int _pageSize = 5;

        public AdminTeacherController(IAdminTeacherService adminTeacherService, IWebHostEnvironment webHostEnvironment)
        {
            this._rootPath = webHostEnvironment.WebRootPath;
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
            return View("Update", dto);
        }

        [HttpGet("duplicate")]
        public async Task<ActionResult> Duplicate()
        {

            using (var stream = new FileStream(Path.Combine(_rootPath, "files", "template.xlsx"), FileMode.Open))
            {
                byte[] file = new byte[stream.Length];
                await stream.ReadAsync(file, 0, file.Length);
                return new FileContentResult(file,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    FileDownloadName = $"brands_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                };
            }

        }
    }
}
