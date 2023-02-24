namespace RegistanFerghanaLC.Service.Intefaces.Common
{
    public interface IIdentityService
    {
        public long? Id { get; }

        public string FirstName { get; }

        public string LastName { get; }
        public string Image { get; }
        public string PhoneNumber { get; }
    }
}
