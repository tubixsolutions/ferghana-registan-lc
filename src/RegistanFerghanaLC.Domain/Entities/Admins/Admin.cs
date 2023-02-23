namespace RegistanFerghanaLC.Domain.Entities.Users
{
    public class Admin : Human
    {
        public string Address { get; set; } = String.Empty;

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;
    }
}
