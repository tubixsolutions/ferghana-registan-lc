using RegistanFerghanaLC.Service.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Dtos.ExtraLesson
{
    public class ExtraLessonDetailsUpdateDto
    {
        [Required]
        public bool IsDone { get; set; }

        [Required,Rank]
        public int Rank { get; set; }

        [Required]
        public string Comment { get; set; } = String.Empty;
    }
}
