using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Service.Intefaces.Common;

namespace RegistanFerghanaLC.Service.Service.Common
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _accessor;
        public IdentityService(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }
        public long? Id
        {
            get
            {
                var res = _accessor.HttpContext!.User.FindFirst("Id");
                return res is not null ? long.Parse(res.Value) : null;
            }
        }

        public string FirstName
        {
            get
            {
                var result = _accessor.HttpContext!.User.FindFirst("FirstName");
                return (result is null) ? String.Empty : result.Value;
            }
        }
        public string LastName
        {
            get
            {
                var result = _accessor.HttpContext!.User.FindFirst("LastName");
                return (result is null) ? String.Empty : result.Value;
            }
        }

        public string Image
        {
            get
            {
                var result = _accessor.HttpContext!.User.FindFirst("Image");
                return (result is null) ? String.Empty : result.Value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                var result = _accessor.HttpContext!.User.FindFirst("PhoneNumber");
                return (result is null) ? String.Empty : result.Value;
            }
        }
    }
}
