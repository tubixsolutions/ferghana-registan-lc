using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.ViewModels.TeacherViewModels
{
    public class TeacherByLessonsNumberViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public string ImagePath { get; set; } = String.Empty;

        public string PhoneNumber { get; set; } = String.Empty;

        public int LessonsNumber { get; set; }
        public double AverageRank { get; set; }
    }
}
