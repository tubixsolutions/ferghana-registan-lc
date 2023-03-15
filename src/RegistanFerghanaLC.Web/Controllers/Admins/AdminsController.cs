using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.DataAccess.Migrations;
using RegistanFerghanaLC.Domain.Constants;
using RegistanFerghanaLC.Domain.Entities.Users;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Dtos.Admins;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.ViewModels.AdminViewModels;

namespace RegistanFerghanaLC.Web.Controllers.Admins
{
    [Route("admins")]
    //[Authorize(Roles = "SuperAdmin")]
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
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            List<AdminViewModel> admins;
            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.AdminSearch = search;
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
        public async Task<IActionResult> GetAsync(int someId)
        {
            var admin = await _adminService.GetByIdAsync(someId);
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
            var adminId = (int)_identityService.Id!.Value;
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

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromForm] AdminUpdateDto adminUpdateDto, int adminId)
        {
            var admin = await _adminService.UpdateAsync(adminId, adminUpdateDto);
            if (admin) return RedirectToAction("Update", "admins");
            else return RedirectToAction("Update", "admins");
        }

        [HttpPost("updateImage")]
        public async Task<IActionResult> UpdateImageAsync([FromForm] IFormFile formFile)
        {
            var updateImage = await _adminService.UpdateImageAsync((int)_identityService.Id!, formFile);
            return await UpdateAsync();
        }

        [HttpPost("passwordUpdate")]
        public async Task<IActionResult> PasswordUpdateAsync(int id, PasswordUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdatePasswordAsync(id, dto);
                if (result) return await UpdateAsync();
                else return await UpdateAsync();
            }
            else return await UpdateAsync();
        }
        #endregion

        #region DeleteImage
        [HttpPost("deleteImage")]
        public async Task<IActionResult> DeleteImageAsync()
        {
            var image = await _adminService.DeleteImageAsync((int)_identityService.Id!);
            return await UpdateAsync();
        }
        #endregion
    }
}
