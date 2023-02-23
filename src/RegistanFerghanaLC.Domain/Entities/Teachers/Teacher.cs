using RegistanFerghanaLC.Domain.Enums;

namespace RegistanFerghanaLC.Domain.Entities.Teachers
{
    public class Teacher : Human
    {
        public PartOfDay PartOfDay { get; set; } = PartOfDay.FirstPartOfDay;

        public bool WorkDays { get; set; } = true;

        public string PasswordHash { get; set; } = String.Empty;
        
        public string Salt { get; set; } = String.Empty;

        public EnglishLevel TeacherLevel { get; set; }

        public string Subject { get; set; } = String.Empty;
    }
}
