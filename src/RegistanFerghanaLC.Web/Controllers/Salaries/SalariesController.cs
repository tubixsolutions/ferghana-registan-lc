using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Common.Utils;
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
        public async Task<ViewResult> Index(int page=1)
        {
            var reults = await _salaryService.GetAllAsync(new PaginationParams(page,_pageSize));
            return View("Index", reults);
        }
    }
}
