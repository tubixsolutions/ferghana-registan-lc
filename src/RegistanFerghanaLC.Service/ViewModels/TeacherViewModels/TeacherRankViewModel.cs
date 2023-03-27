using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.ViewModels.TeacherViewModels
{
    public class TeacherRankViewModel
    {
        public int Id { get; set; }

        public int One { get; set; } = 0;

        public int Two { get; set; } = 0;

        public int Three { get; set; } = 0;

        public int Four { get; set; } = 0;

        public int Five { get; set; } = 0;

        public int LessonsNumber { get; set; }
        public double AverageRank { get; set; }
    }
}
