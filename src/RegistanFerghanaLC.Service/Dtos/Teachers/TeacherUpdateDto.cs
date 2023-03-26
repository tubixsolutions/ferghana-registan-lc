using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Domain.Common;
using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Teachers
{
    public class TeacherUpdateDto
    {
        public int Id { get; set; } 

        [Required (ErrorMessage = "Please enter the Firstname of the teacher!")]
        public string FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter the LastName of the teacher!")]
        public string LastName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter the Description of the teacher!")]
        public string Description { get; set; } = String.Empty;

        [AllowedFiles(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" }), MaxFile(2)]
        public IFormFile? Image { get; set; }
        public string ImagePath { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Please enter the work days of the teacher!")]
        public bool WorkDays { get; set; } = true;

        [Required(ErrorMessage = "Please enter the Phone number of the teacher!")]
        public string PhoneNumber { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter the level of the teacher!")]
        public string TeacherLevel { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter the Birth date of the teacher!")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter the subject of the teacher!")]
        public string Subject { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter the work time of the teacher!")]
        public PartOfDay PartOfDay { get; set; }

        public static implicit operator Teacher(TeacherUpdateDto dto)
        {
            return new Teacher()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Description = dto.Description,
                Image = dto.ImagePath,
                WorkDays = dto.WorkDays,
                PhoneNumber = dto.PhoneNumber,
                TeacherLevel = dto.TeacherLevel,
                BirthDate = dto.BirthDate,
                Subject = dto.Subject,
                PartOfDay = dto.PartOfDay,
            };
        }
    }
}
