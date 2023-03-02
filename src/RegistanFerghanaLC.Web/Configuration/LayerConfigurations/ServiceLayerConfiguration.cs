﻿using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Service.Interfaces.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Salaries;
using RegistanFerghanaLC.Service.Services.AccountService;
using RegistanFerghanaLC.Service.Services.AdminService;
using RegistanFerghanaLC.Service.Services.Common;
using RegistanFerghanaLC.Service.Services.SalaryService;

namespace RegistanFerghanaLC.Web.Configuration.LayerConfigurations
{
    public static class ServiceLayerConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdminStudentService, AdminStudentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped<IAdminTeacherService, AdminTeacherService>();

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MappingConfiguration));

        }
    }
}
