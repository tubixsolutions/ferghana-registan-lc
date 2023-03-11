using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Service.Interfaces.Accounts;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.ExtraLesson;
using RegistanFerghanaLC.Service.Interfaces.Salaries;
using RegistanFerghanaLC.Service.Interfaces.Students;
using RegistanFerghanaLC.Service.Interfaces.Teachers;
using RegistanFerghanaLC.Service.Services.AccountService;
using RegistanFerghanaLC.Service.Services.AdminService;
using RegistanFerghanaLC.Service.Services.Common;
using RegistanFerghanaLC.Service.Services.ExtraLessonService;
using RegistanFerghanaLC.Service.Services.SalaryService;
using RegistanFerghanaLC.Service.Services.StudentService;
using RegistanFerghanaLC.Service.Services.TeacherService;

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
            services.AddScoped<IExtraLessonService, ExtraLessonService>();
            services.AddScoped<IExtraLessonDetailsService, ExtraLessonDetailsService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAdminSubjectService , AdminSubjectService>();
            services.AddScoped<ITeacherService, TeacherService>();

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(MappingConfiguration));

        }
    }
}
