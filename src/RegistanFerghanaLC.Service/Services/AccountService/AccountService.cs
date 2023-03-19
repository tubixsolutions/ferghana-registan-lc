using AutoMapper;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Constants;
using RegistanFerghanaLC.Domain.Entities.Users;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Common.Security;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Admins;
using RegistanFerghanaLC.Service.Interfaces.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Common;

namespace RegistanFerghanaLC.Service.Services.AccountService;
public class AccountService : IAccountService
{
    private readonly IUnitOfWork _repository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public AccountService(IUnitOfWork unitOfWork, IAuthService authService, IMapper mapper, IImageService imageService)
    {
        this._repository = unitOfWork;
        this._authService = authService;
        this._mapper = mapper;
        this._imageService = imageService;
    }

    public async Task<bool> AdminRegisterAsync(AdminRegisterDto adminRegisterDto)
    {
        var phoneNumberCheck = await _repository.Admins.FirstOrDefault(x => x.PhoneNumber == adminRegisterDto.PhoneNumber);
        if (phoneNumberCheck is not null)
            throw new AlreadyExistingException(nameof(adminRegisterDto.PhoneNumber), "This phone number is already registered.");

        var hashresult = PasswordHasher.Hash(adminRegisterDto.Password);
        var admin = _mapper.Map<Admin>(adminRegisterDto);
        admin.AdminRole = Role.Admin;
        admin.PasswordHash = hashresult.Hash;
        admin.Salt = hashresult.Salt;
        admin.CreatedAt = TimeHelper.GetCurrentServerTime();
        admin.LastUpdatedAt = TimeHelper.GetCurrentServerTime();
        _repository.Admins.Add(admin);
        var result = await _repository.SaveChangesAsync();
        return result > 0;
    }

    public async Task<string> LoginAsync(AccountLoginDto accountLoginDto)
    {
        var admin = await _repository.Admins.FirstOrDefault(x => x.PhoneNumber == accountLoginDto.PhoneNumber);
        if (admin is null)
        {
            var teacher = await _repository.Teachers.FirstOrDefault(x=>x.PhoneNumber == accountLoginDto.PhoneNumber);
            if(teacher is null)
            {
                var student = await _repository.Students.FirstOrDefault(x => x.PhoneNumber == accountLoginDto.PhoneNumber);
                if (student is null) throw new NotFoundException(nameof(accountLoginDto.PhoneNumber), "No user with this phone number is found!");
                else
                {
                    var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, student.Salt, student.PasswordHash);
                    if (hasherResult)
                    {
                        string token = _authService.GenerateToken(student, "student");
                        return token;
                    }
                    else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
                }
            }
            else
            {
                var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, teacher.Salt, teacher.PasswordHash);
                if (hasherResult)
                {
                    string token = _authService.GenerateToken(teacher, "teacher");
                    return token;
                }
                else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
            }
            
        }
        else
        {
            var hasherResult = PasswordHasher.Verify(accountLoginDto.Password, admin.Salt, admin.PasswordHash);
            if (hasherResult)
            {
                string token = "";
                if(admin.PhoneNumber != null)
                {
                    token = _authService.GenerateToken(admin, "admin");
                    return token;
                }
                token = _authService.GenerateToken(admin, "admin");
                return token;
            }
            else throw new NotFoundException(nameof(accountLoginDto.Password), "Incorrect password!");
        }
    }
}
