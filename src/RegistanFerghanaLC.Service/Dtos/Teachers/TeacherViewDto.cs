using RegistanFerghanaLC.Domain.Common;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Dtos.FileViewModels;

namespace RegistanFerghanaLC.Service.Dtos.Teachers
{
    public class TeacherViewDto : BaseEntity
    {
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string? ImagePath { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime BirthDate { get; set; }
        public PartOfDay PartOfDay { get; set; } = PartOfDay.FirstPartOfDay;

        public bool WorkDays { get; set; } = true;

        public string TeacherLevel { get; set; } = String.Empty;

        public string Subject { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; }

        public FileModeldto FileModel { get; set; }
    }
}
