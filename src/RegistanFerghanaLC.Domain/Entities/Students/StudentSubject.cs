using RegistanFerghanaLC.Domain.Common;

namespace RegistanFerghanaLC.Domain.Entities.Students
{
    public class StudentSubject : Auditable
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; } = default!;

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; } = default!;
    }
}
