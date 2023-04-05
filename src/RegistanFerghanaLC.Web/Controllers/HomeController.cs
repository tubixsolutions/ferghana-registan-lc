using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Web.Models;
using System.Diagnostics;

namespace RegistanFerghanaLC.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminHomeService _adminHomeService;

        public HomeController(ILogger<HomeController> logger, IAdminHomeService adminHomeService)
        {
            this._logger = logger;
            this._adminHomeService = adminHomeService;
        }

        public async Task<IActionResult> Index()
        {
            var topTeachers = await _adminHomeService.GetTopTeachersByRankAsync();
            return View("Index", topTeachers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}