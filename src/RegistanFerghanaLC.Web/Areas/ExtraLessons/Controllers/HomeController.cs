using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Interfaces.ExtraLesson;

namespace RegistanFerghanaLC.Web.Areas.ExtraLessons.Controllers
{
    [Route("extraLesson/home")]
    [ApiController]
    public class HomeController : BaseController
    {
        private readonly IExtraLessonService  _repository;
        private readonly int _pageSize = 5;

        public HomeController(IExtraLessonService service)
        {
            this._repository = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int teacherId, int page =1 )
        {
            return Ok(await _repository.GetAllByDateAsync(teacherId, new PaginationParams(page, _pageSize)));
        }
    }
}
