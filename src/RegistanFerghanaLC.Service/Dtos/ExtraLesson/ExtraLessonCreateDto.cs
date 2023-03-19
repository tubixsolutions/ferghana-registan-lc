using RegistanFerghanaLC.Service.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.ExtraLesson
{
    public class ExtraLessonCreateDto
    {
        [Required]
        public string LessonTopic { get; set; } = string.Empty;

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        [StartTime]
        public string StartTime { get; set; } = string.Empty;
    }
}
