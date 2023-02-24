namespace RegistanFerghanaLC.Service.Interfaces.Common
{
    public interface IIdentityService
    {
        public long? Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string PhoneNumber { get; }

        public string ImagePath { get; }
    }
}
