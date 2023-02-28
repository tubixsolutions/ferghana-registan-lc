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
        public int Rank { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
