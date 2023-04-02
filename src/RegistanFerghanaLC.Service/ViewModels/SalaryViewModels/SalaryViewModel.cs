using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.ViewModels.SalaryViewModels
{
    public class SalaryViewModel
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LessonTopic { get; set; } = string.Empty;
        public int Rank { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
