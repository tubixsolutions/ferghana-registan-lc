using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Students;

namespace RegistanFerghanaLC.Web.Areas.Students.Controllers
{
    [Route("students/home")]
    public class HomeController : BaseController
    {
        private readonly int _pageSize = 1;
        private readonly IAdminStudentService _adminstudentService;
        private readonly IStudentService _studentService;
        public HomeController(IAdminStudentService adminStudentService, IStudentService studentService)
        {
            this._adminstudentService = adminStudentService;
            this._studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1) 
            => Ok(await _adminstudentService.GetAllAsync(new PaginationParams(page, _pageSize)));


        [HttpGet("subject")]
        public async Task<IActionResult> DeleteAsync(string subject, int page = 1) 
            => Ok(await _studentService.GetAllTeacherBySubjectAsync(subject, new PaginationParams(page, _pageSize)));
    }
}
