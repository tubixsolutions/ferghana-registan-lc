using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Intefaces.Common;
using RegistanFerghanaLC.Service.ViewModels.Accounts;

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
                FirstName = _identityService.FirstName,
                LastName = _identityService.LastName,
                PhoneNumber= _identityService.PhoneNumber,
                Image = _identityService.Image
            };
            return View(accountBaseViewModel);
        }
    }
}
