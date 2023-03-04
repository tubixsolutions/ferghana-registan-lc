using RegistanFerghanaLC.Domain.Enums;

namespace RegistanFerghanaLC.Service.Dtos.Teachers
{
    public class TeacherViewDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string? Image { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime BirthDate { get; set; }
        public PartOfDay PartOfDay { get; set; } = PartOfDay.FirstPartOfDay;

        public bool WorkDays { get; set; } = true;

        public EnglishLevel TeacherLevel { get; set; }

        public string Subject { get; set; } = String.Empty;
    }
}
