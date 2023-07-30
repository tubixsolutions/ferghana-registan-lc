using RegistanFerghanaLC.Service.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.ExtraLesson
{
    public class ExtraLessonDetailsUpdateDto
    {
        [Required]
        public bool IsDone { get; set; }

        [Required, Rank]
        public int Rank { get; set; }

        [Required]
        public string Comment { get; set; } = String.Empty;
    }
}
