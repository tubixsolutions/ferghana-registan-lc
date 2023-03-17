using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistanFerghanaLC.DataAccess.Migrations;
using RegistanFerghanaLC.Domain.Constants;
using RegistanFerghanaLC.Domain.Entities.Users;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Dtos.Admins;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Services.AdminService;
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

        #region GetPhoneNumber
        [HttpGet("phoneNumber")]
        public async Task<IActionResult> GetByPhoneNumberAsync(string phoneNumber)
        {
            var admin = await _adminService.GetByPhoneNumberAsync(phoneNumber);
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

            return View("Profile", adminView);
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
        [HttpGet("delete")]
        public async Task<ViewResult> DeleteAsync(int adminId)
        {
            var admin = await _adminService.GetByIdAsync(adminId);
            if (admin != null) return View("Delete", admin);
            else return View("admins");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAdminAsync(int adminId)
        {
            var admin = await _adminService.DeleteAsync(adminId);
            if (admin) return RedirectToAction("index", "admins", new { area = "" });
            else return View();
        }

        [HttpPost("deleteImage")]
        public async Task<IActionResult> DeleteImageAsync()
        {
            var image = await _adminService.DeleteImageAsync((int)_identityService.Id!);
            return await UpdateAsync();
        }
        #endregion

    }
}
