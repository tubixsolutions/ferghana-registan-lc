using RegistanFerghanaLC.Domain.Common;
using RegistanFerghanaLC.Domain.Entities.Users;

namespace RegistanFerghanaLC.Service.ViewModels.AdminViewModels
{
    public class AdminViewModel : BaseEntity
    {
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string ImagePath { get; set; } = String.Empty;
        
        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime BirthDate { get; set; } = default!;

        public string Address { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; } = default!;

        public static implicit operator AdminViewModel(Admin model)
        {
            return new AdminViewModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImagePath = model.Image!,
                PhoneNumber = model.PhoneNumber,
                BirthDate = model.BirthDate,
                Address = model.Address,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
