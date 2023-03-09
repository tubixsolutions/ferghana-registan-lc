using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.Controllers.Admins;
[Route("adminsubject")]

public class AdminSubjectController : Controller
{
    private readonly IAdminSubjectService _service;

    public AdminSubjectController(IAdminSubjectService service)
    {
        this._service = service;
    }
    [HttpGet]
    public ViewResult Register()
    {
        return View("Register");
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterSubjectAsync(string subject)
    {
        var result = await _service.SubjectCreateAsync(subject);
        if (result)
            return RedirectToAction("index", "home", new { area = "" });
        else
            return Register();
    }
}
