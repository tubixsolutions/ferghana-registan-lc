using RegistanFerghanaLC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Dtos.Teachers
{
    public class TeacherViewDto
    {
        public PartOfDay PartOfDay { get; set; } = PartOfDay.FirstPartOfDay;

        public bool WorkDays { get; set; } = true;

        public EnglishLevel TeacherLevel { get; set; }

        public string Subject { get; set; } = String.Empty;
    }
}
