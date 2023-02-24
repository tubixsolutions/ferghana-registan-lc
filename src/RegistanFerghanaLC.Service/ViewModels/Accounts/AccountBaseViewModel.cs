namespace RegistanFerghanaLC.Service.ViewModels.Accounts
{
    public class AccountBaseViewModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string? Image { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;
    }
}
