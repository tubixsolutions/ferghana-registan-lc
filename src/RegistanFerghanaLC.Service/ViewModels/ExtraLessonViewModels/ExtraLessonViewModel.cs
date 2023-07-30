namespace RegistanFerghanaLC.Service.ViewModels.ExtraLessonViewModels
{
    public class ExtraLessonViewModel
    {
        public int Id { get; set; }
        public string LessonTopic { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
