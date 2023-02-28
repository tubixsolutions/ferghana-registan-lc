using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.ViewModels;

namespace RegistanFerghanaLC.Web.ViewComponents
{
    public class IdentityViewComponent : ViewComponent
    {
        private readonly IIdentityService _identityService;
        public IdentityViewComponent(IIdentityService identity)
        {
            this._identityService = identity;
        }
        public IViewComponentResult Invoke()
        {
            AccountBaseViewModel accountBaseViewModel = new AccountBaseViewModel()
            {
                Id = _identityService.Id!.Value,
                PhoneNumber = _identityService.PhoneNumber,
                FirstName = _identityService.FirstName,
                LastName = _identityService.LastName,
                Image = _identityService.ImagePath
            };
            return View(accountBaseViewModel);
        }
    }
}
