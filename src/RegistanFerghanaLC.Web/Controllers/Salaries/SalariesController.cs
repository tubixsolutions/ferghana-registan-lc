using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Helpers;
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
        [HttpGet]
        public async Task<ViewResult> Index(int page = 1)
        {
            var res = await _salaryService.GetAllAsync(new PaginationParams(page, _pageSize));
            var results = new SoftDto()
            {
                Salaries = res,
                StartDate = ("2023-02-25"),
                EndDate = TimeHelper.GetCurrentServerTime().ToString("yyyy-MM-dd")
            };
            return View("Index", results);
        }

        [HttpPost("GetAllByDate")]
        public async Task<ViewResult> GetAllByDateAsync(SoftDto softDto)
        {
            if (ModelState.IsValid)
            {
                var res = await _salaryService.GetAllByDateAsync(new PaginationParams(softDto.ChoosenPage, _pageSize), DateTime.Parse(softDto.StartDate), DateTime.Parse(softDto.EndDate));
                var results = new SoftDto()
                {
                    StartDate = softDto.StartDate,
                    EndDate = softDto.EndDate,
                    Salaries = res
                };
                return View("Index", results);
            }
            else return View("Index");
        }

        [HttpGet("GetAllByDateGet")]
        public async Task<ViewResult> GetAllByDateGetAsync(string start, string end, int page = 1)
        {
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            if (startDate == TimeHelper.GetCurrentServerTime() && endDate == TimeHelper.GetCurrentServerTime() || endDate < startDate || startDate == endDate)
            {
                var results = await _salaryService.GetAllAsync(new PaginationParams(ViewBag.page, _pageSize));
                ViewBag.start = TimeHelper.GetCurrentServerTime().ToString("yyyy-MM-dd");
                ViewBag.end = TimeHelper.GetCurrentServerTime().ToString("yyyy-MM-dd");
                var res = new SoftDto()
                {
                    Salaries = results,
                    StartDate = start,
                    EndDate = end,
                };
                return View("Index", res);
            }
            else
            {
                var results = await _salaryService.GetAllByDateAsync(new PaginationParams(page, _pageSize), startDate, endDate);
                ViewBag.start = startDate.ToString("yyyy-MM-dd");
                ViewBag.end = endDate.ToString("yyyy-MM-dd");
                var res = new SoftDto()
                {
                    Salaries = results,
                    StartDate = start,
                    EndDate = end,
                };
                return View("Index", res);
            }
        }

        [HttpGet("{teacherId}")]
        public async Task<ViewResult> GetAllByIdAsync(int teacherId, string teacherName, int page = 1)
        {
            ViewBag.teacherName = teacherName;
            var results = await _salaryService.GetAllByIdAsync(teacherId, new PaginationParams(page, _pageSize));
            return View("GetAllById", results);
        }
    }
}
