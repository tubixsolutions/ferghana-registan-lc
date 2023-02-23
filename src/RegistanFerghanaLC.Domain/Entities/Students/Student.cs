using RegistanFerghanaLC.Domain.Enums;

namespace RegistanFerghanaLC.Domain.Entities.Students
{
    public class Student : Human
    {
        public byte WeeklyLimit { get; set; }

        public EnglishLevel StudentLevel { get; set; } = EnglishLevel.Beginner;

        public string PasswordHash { get; set; } = String.Empty;

        public string Salt { get; set; } = String.Empty;
    }
}
