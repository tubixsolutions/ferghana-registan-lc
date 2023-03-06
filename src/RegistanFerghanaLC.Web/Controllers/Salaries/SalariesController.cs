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
        private readonly int _pageSize = 2;
        public SalariesController(ISalaryService salaryService)
        {
            this._salaryService = salaryService;
        }
        public async Task<ViewResult> Index(int page = 1)
        {
            var results = await _salaryService.GetAllAsync(new PaginationParams(page,_pageSize));
            ViewBag.start = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.end = DateTime.Now.ToString("yyyy-MM-dd");
            
            return View("Index", results);
        }

        [HttpGet("GetAllByDate")]
        public async Task<ViewResult> GetAllByDateAsync(int page = 1)
        {
            var startDate = DateTime.Parse(Request.QueryString.Value![11..21]); 
            var endDate = DateTime.Parse(Request.QueryString.Value[30..40]);
            if (startDate == DateTime.Now && endDate == DateTime.Now || endDate < startDate)
            {
                var results = await _salaryService.GetAllAsync(new PaginationParams(ViewBag.page, _pageSize)); 
                ViewBag.start = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.end = DateTime.Now.ToString("yyyy-MM-dd");
                return View("Index", results);
            }
            else
            {
                var results = await _salaryService.GetAllByDateAsync(new PaginationParams(page, _pageSize), startDate, endDate);
                ViewBag.start = startDate.ToString("yyyy-MM-dd"); 
                ViewBag.end = endDate.ToString("yyyy-MM-dd");
                return View("Index", results);
            }

        }

        [HttpGet("{teacherId}")]
        public async Task<ViewResult> GetAllByIdAsync(int teacherId, int page = 1)
        {
            var results = await _salaryService.GetAllByIdAsync(teacherId, new PaginationParams(page,_pageSize));
            return View("GetAllById", results);
        }
    }
}
