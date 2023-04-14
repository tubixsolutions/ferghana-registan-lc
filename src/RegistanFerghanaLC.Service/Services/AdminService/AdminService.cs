using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Dtos.Admins;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Files;
using RegistanFerghanaLC.Service.Services.Common;
using RegistanFerghanaLC.Service.ViewModels.AdminViewModels;

namespace RegistanFerghanaLC.Service.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityService _identityService;
        private readonly IFileService _fileService;

        public AdminService(IUnitOfWork unitOfWork, IIdentityService identityService, IFileService fileService)
        {
            this._unitOfWork = unitOfWork;
            this._identityService = identityService;
            this._fileService = fileService;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            if (admin is null) throw new NotFoundException("Admin", $"{id} not found");
            _unitOfWork.Admins.Delete(id);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteImageAsync(int adminId)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(adminId);
            if (admin is null) throw new NotFoundException("Admin", $"{adminId} not found");
            else
            {
                await _fileService.DeleteImageAsync(admin.Image!);
                admin.Image = "";
                _unitOfWork.Admins.Update(adminId, admin);
                var res = await _unitOfWork.SaveChangesAsync();
                return res > 0;
            }
        }

        public async Task<List<AdminViewModel>> GetAllAsync(string search)
        {
            var query = _unitOfWork.Admins.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.FirstName.ToLower().StartsWith(search.ToLower()) || x.LastName.ToLower().StartsWith(search.ToLower()) || x.Address.ToLower().StartsWith(search.ToLower()));
            }

            var result = await query.OrderByDescending(x => x.CreatedAt).Select(x => (AdminViewModel)x).ToListAsync();
            return result;
        }

        public Task<List<AdminViewModel>> GetAllAsync()
        {
            var query = _unitOfWork.Admins.GetAll().OrderByDescending(x => x.CreatedAt).Select(x => (AdminViewModel)x).ToListAsync();
            return query;
        }

        public async Task<AdminViewModel> GetByIdAsync(int id)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            if (admin is null) throw new NotFoundException("Admin", $"{id} not found");
            var adminView = (AdminViewModel)admin;
            return adminView;
        }

        public async Task<AdminViewModel> GetByPhoneNumberAsync(string phoneNumber)
        {
            var admin = await _unitOfWork.Admins.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
            if (admin is null) throw new NotFoundException("Admin", $"{phoneNumber} not found");
            var adminView = (AdminViewModel)admin;
            return adminView;
        }

        public async Task<bool> UpdateAsync(int id, AdminUpdateDto adminUpdatedDto)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            if (admin is null) throw new NotFoundException("Admin", $"{id} not found");
            _unitOfWork.Admins.TrackingDeteched(admin);
            if (adminUpdatedDto != null)
            {
                admin.FirstName = String.IsNullOrEmpty(adminUpdatedDto.FirstName) ? admin.FirstName : adminUpdatedDto.FirstName;
                admin.LastName = String.IsNullOrEmpty(adminUpdatedDto.LastName) ? admin.LastName : adminUpdatedDto.LastName;
                admin.Image = String.IsNullOrEmpty(adminUpdatedDto.ImagePath) ? admin.Image : adminUpdatedDto.ImagePath;
                admin.PhoneNumber = String.IsNullOrEmpty(adminUpdatedDto.PhoneNumber) ? admin.PhoneNumber : adminUpdatedDto.PhoneNumber;
                admin.BirthDate = admin.BirthDate;
                admin.Address = String.IsNullOrEmpty(adminUpdatedDto.Address) ? admin.Address : adminUpdatedDto.Address;
                if (adminUpdatedDto.Image is not null)
                {
                    admin.Image = await _fileService.UploadImageAsync(adminUpdatedDto.Image);
                }
                admin.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
                _unitOfWork.Admins.Update(id, admin);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else throw new ModelErrorException("", "Not found");
        }

        public async Task<bool> UpdateImageAsync(int id, IFormFile formFile)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            var updateImage = await _fileService.UploadImageAsync(formFile);
            var adminUpdatedDto = new AdminUpdateDto()
            {
                ImagePath = updateImage
            };
            var result = await UpdateAsync(id, adminUpdatedDto);
            return result;
        }

        public async Task<bool> UpdatePasswordAsync(int id, PasswordUpdateDto dto)
        {
            var admin = await _unitOfWork.Admins.FindByIdAsync(id);
            if (admin is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Admin is not found");
            _unitOfWork.Admins.TrackingDeteched(admin);
            var res = PasswordHasher.Verify(dto.OldPassword, admin.Salt, admin.PasswordHash);
            if (res)
            {
                if (dto.NewPassword == dto.VerifyPassword)
                {
                    var hash = PasswordHasher.Hash(dto.NewPassword);
                    admin.PasswordHash = hash.Hash;
                    admin.Salt = hash.Salt;
                    _unitOfWork.Admins.Update(id, admin);
                    var result = await _unitOfWork.SaveChangesAsync();
                    return result > 0;
                }
                else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "new password and verify" + " password must be match!");
            }
            else throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Invalid Password");
        }
    }
}
