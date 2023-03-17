using RegistanFerghanaLC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Dtos.Teachers
{
    public class TeacherUpdateDto
    {
        [Required (ErrorMessage = "Please enter the Firstname of the teacher!")]
        public string FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter the LastName of the teacher!")]
        public string LastName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter the picture of the teacher!")]
        public string? Image { get; set; }
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


    }
}
