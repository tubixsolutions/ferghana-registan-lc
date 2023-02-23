using RegistanFerghanaLC.Domain.Entities;

namespace RegistanFerghanaLC.Service.Interfaces.Common;
public interface IAuthService
{
    public string GenerateToken(Human human, string role);

}
