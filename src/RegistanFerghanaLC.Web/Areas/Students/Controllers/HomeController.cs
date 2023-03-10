using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.ExtraLesson;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.ExtraLesson;
using RegistanFerghanaLC.Service.Interfaces.Students;

namespace RegistanFerghanaLC.Web.Areas.Students.Controllers
{
    [Route("students")]
    public class HomeController : BaseController
    {
        private readonly int _pageSize = 1;
        private readonly IAdminStudentService _adminstudentService;
        private readonly IStudentService _studentService;
        private readonly IExtraLessonService _extraLessonService;
        public HomeController(IAdminStudentService adminStudentService, IStudentService studentService, IExtraLessonService extraLessonService)
        {
            this._adminstudentService = adminStudentService;
            this._studentService = studentService;
            this._extraLessonService = extraLessonService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1) 
            => Ok(await _adminstudentService.GetAllAsync(new PaginationParams(page, _pageSize)));


        [HttpGet("subject")]

        public async Task<IActionResult> GetTeachersBySubjectAsync(string subject, int page = 1) 
            => Ok(await _studentService.GetAllTeacherBySubjectAsync(subject, new PaginationParams(page, _pageSize)));

        [HttpPost("extraLesson")]
        public async Task<IActionResult> CreateExtraLessonAsync([FromForm] ExtraLessonCreateDto createDto)
            => Ok(await _extraLessonService.CreateAsync(createDto));

        [HttpGet("limit")]
        public async Task<IActionResult> GetLimitStudentAsync(int id) 
            => Ok(await _studentService.GetLimitStudentAsync(id));
    }
}
