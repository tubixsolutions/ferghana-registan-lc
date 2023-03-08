using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegistanFerghanaLC.Web.Areas.Teachers.Controllers
{
    [Route("teachers/home")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index() => Ok("Ishladi");
    }
}
