using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.ViewModels.TeacherViewModels
{
    public class TeacherBySubjectViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool WorkDays { get; set; }
        public string TeacherLevel { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = string.Empty;
    }
}
