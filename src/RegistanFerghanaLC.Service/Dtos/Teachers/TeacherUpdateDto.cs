using RegistanFerghanaLC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Dtos.Teachers
{
    public class TeacherUpdateDto
    {
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string? Image { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime BirthDate { get; set; }

        public string Subject { get; set; } = String.Empty;

    }
}
