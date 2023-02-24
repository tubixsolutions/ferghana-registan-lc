using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Service.Interfaces.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Services.AccountService;
using RegistanFerghanaLC.Service.Services.Common;

namespace RegistanFerghanaLC.Web.Configuration.LayerConfigurations
{
    public static class ServiceLayerConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
           
        }
    }
}
