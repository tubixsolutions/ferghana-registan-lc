using RegistanFerghanaLC.Domain.Constants;

namespace RegistanFerghanaLC.Domain.Entities.Users
{
    public class Admin : Human
    {
        public string Address { get; set; } = String.Empty;

        public Role AdminRole { get; set; } = Role.Admin;

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;
    }
}
