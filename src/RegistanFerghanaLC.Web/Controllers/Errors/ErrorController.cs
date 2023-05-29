using Microsoft.AspNetCore.Mvc;

namespace RegistanFerghanaLC.Web.Controllers.Errors
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error")]
        public IActionResult Error()
        {
            return View("Error");
        }
    }

}
