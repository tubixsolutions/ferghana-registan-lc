namespace RegistanFerghanaLC.Domain.Entities.ExtraLessons
{
    public class ExtraLessonDetails : ExtraLesson
    {
        public bool IsDone { get; set; }

        public int Rank { get; set; }

        public string Comment { get; set; } = String.Empty;

        public int ExtraLessonId { get; set; }
        public virtual ExtraLesson ExtraLesson { get; set; } = default!;
    }
}
