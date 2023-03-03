using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Salaries;
using RegistanFerghanaLC.Service.Interfaces.Salaries;

namespace RegistanFerghanaLC.Web.Controllers.Salaries
{
    [Route("Salaries")]
    public class SalariesController : Controller
    {
        private readonly ISalaryService _salaryService;
        private readonly int _pageSize = 5;
        public SalariesController(ISalaryService salaryService)
        {
            this._salaryService = salaryService;
        }
        public async Task<ViewResult> Index(int page = 1)
        {
            var results = await _salaryService.GetAllAsync(new PaginationParams(page,_pageSize));
            return View("Index", results);
        }

        [HttpGet("GetAllByDate")]
        public async Task<ViewResult> GetAllByDateAsync(int page = 1)
        {
            var startDate = DateTime.Parse(Request.QueryString.Value![11..21]); 
            var endDate = DateTime.Parse(Request.QueryString.Value[30..40]);
            var results = await _salaryService.GetAllByDateAsync(new PaginationParams(page, _pageSize), startDate, endDate);
            return View("Index", results);
        }

        [HttpGet("{teacherId}")]
        public async Task<ViewResult> GetAllByIdAsync(int teacherId, int page = 1)
        {
            var results = await _salaryService.GetAllByIdAsync(teacherId, new PaginationParams(page,_pageSize));
            return View("GetAllById", results);
        }
    }
}
