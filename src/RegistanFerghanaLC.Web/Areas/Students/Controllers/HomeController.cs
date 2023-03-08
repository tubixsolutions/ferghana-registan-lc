using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Areas.Students.Controllers
{
    [Route("students/home")]
    public class HomeController : BaseController
    {
        private readonly int _pageSize = 1;
        private readonly IAdminStudentService _studentService;

        public HomeController(IAdminStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1) 
            => Ok(await _studentService.GetAllAsync(new PaginationParams(page, _pageSize)));


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) 
            => Ok(await _studentService.DeleteAsync(id));
    }
}
