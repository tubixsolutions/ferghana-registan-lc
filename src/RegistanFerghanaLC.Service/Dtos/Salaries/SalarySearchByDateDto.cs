using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Dtos.Salaries
{
    public class SalarySearchByDateDto
    {
        [Required(ErrorMessage = "Please enter the start date")]
        public string StartDate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the end date")]
        public string EndDate { get; set; } = string.Empty;
    }
}
