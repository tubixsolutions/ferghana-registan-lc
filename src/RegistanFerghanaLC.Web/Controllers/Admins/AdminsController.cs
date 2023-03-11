using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.Service.Dtos.Admins;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.ViewModels.AdminViewModels;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    [Route("admins")]
    public class AdminsController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IIdentityService _identityService;

        public AdminsController(IAdminService adminService, IIdentityService identityService)
        {
            this._adminService = adminService;
            this._identityService = identityService;
        }

        #region GetAll
        public async Task<IActionResult> Index(string search)
        {
            List<AdminViewModel> admins;
            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                admins = await _adminService.GetAllAsync(search);
            }
            else
            {
                admins = await _adminService.GetAllAsync();
            }

            return View(admins);
        }
        #endregion

        #region Get
        [HttpGet("get")]
        public async Task<IActionResult> GetAsync(int adminId)
        {
            var admin = await _adminService.GetByIdAsync(adminId);
            ViewBag.HomeTitle = "Profile";
            var adminView = new AdminViewModel()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                ImagePath = admin.ImagePath,
                PhoneNumber = admin.PhoneNumber,
                BirthDate = admin.BirthDate,
                Address = admin.Address,
                CreatedAt = admin.CreatedAt
            };

            return View("../Admins/Profile", adminView);
        }
        #endregion

        #region Update
        [HttpGet("update")]
        public async Task<ViewResult> UpdateAsync()
        {
            var adminId = (int) _identityService.Id!.Value;
            var admin = await _adminService.GetByIdAsync(adminId);
            ViewBag.adminId = adminId;
            var adminUpdate = new AdminUpdateDto()
            {
                ImagePath = admin.ImagePath,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                PhoneNumber = admin.PhoneNumber,
                BirthDate = admin.BirthDate,
                Address = admin.Address,
            };

            return View("../Admins/Update", adminUpdate);
        }

        //[HttpPost("update")]
        //public async Task<IActionResult> UpdateAsync([FromForm] AdminUpdateDto adminUpdateDto, int adminId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var admin = _adminService.UpdateAsync(adminId, adminUpdateDto);
        //    }
        //}
        #endregion
    }
}
