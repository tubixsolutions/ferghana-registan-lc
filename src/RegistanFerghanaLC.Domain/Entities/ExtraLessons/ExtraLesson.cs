
using RegistanFerghanaLC.Domain.Common;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Entities.Teachers;

namespace RegistanFerghanaLC.Domain.Entities.ExtraLessons
{
    public class ExtraLesson : Auditable
    {
        public string LessonTopic { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = default!;

        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = default!;

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = default!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
