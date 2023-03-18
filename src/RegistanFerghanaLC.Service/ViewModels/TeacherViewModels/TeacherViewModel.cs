using RegistanFerghanaLC.Domain.Entities.Teachers;

namespace RegistanFerghanaLC.Service.ViewModels.TeacherViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string ImagePath { get; set; } = String.Empty;

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime BirthDate { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = default!;

        public static implicit operator TeacherViewModel(Teacher teacher)
        {
            return new TeacherViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                ImagePath = teacher.Image!,
                PhoneNumber = teacher.PhoneNumber,
                BirthDate = teacher.BirthDate,
                CreatedAt = teacher.CreatedAt
            };
        }
    }
}
