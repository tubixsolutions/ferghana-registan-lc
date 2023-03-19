using RegistanFerghanaLC.Domain.Common;
using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Dtos.FileViewModels;
using RegistanFerghanaLC.Service.ViewModels.AdminViewModels;

namespace RegistanFerghanaLC.Service.Dtos.Teachers
{
    public class TeacherViewDto : BaseEntity
    {
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string ImagePath { get; set; } = String.Empty;

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime BirthDate { get; set; }
        public PartOfDay PartOfDay { get; set; } = PartOfDay.FirstPartOfDay;

        public bool WorkDays { get; set; } = true;

        public string TeacherLevel { get; set; } = String.Empty;

        public string Subject { get; set; } = String.Empty;

        public DateTime CreatedAt { get; set; }

        public FileModeldto FileModel { get; set; }

        public static implicit operator TeacherViewDto(Teacher teacher)
        {
            return new TeacherViewDto()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                ImagePath = teacher.Image!,
                PhoneNumber = teacher.PhoneNumber,
                BirthDate = teacher.BirthDate,
                PartOfDay = teacher.PartOfDay,
                WorkDays = teacher.WorkDays,
                TeacherLevel = teacher.TeacherLevel,
                Subject = teacher.Subject,
                CreatedAt = teacher.CreatedAt
            };
        }
    }
}
