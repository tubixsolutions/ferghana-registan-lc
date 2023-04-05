using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Interfaces.Admins;

namespace RegistanFerghanaLC.Web.ViewComponents
{
    public class HomeTopViewComponent : ViewComponent
    {
        private readonly IAdminHomeService _adminHomeService;

        public HomeTopViewComponent(IAdminHomeService adminHomeService)
        {
            this._adminHomeService = adminHomeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _adminHomeService.GetTopTeachersAsync();
            return View(result);
        }
    }
}
