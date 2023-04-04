using RegistanFerghanaLC.Service.ViewModels.TeacherViewModels;

namespace RegistanFerghanaLC.Service.Interfaces.Admins
{
    public interface IAdminHomeService
    {
        public Task<List<TeacherByLessonsNumberViewModel>> GetTopTeachersAsync();
        public Task<List<TeacherByLessonsNumberViewModel>> GetTopTeachersByRankAsync();
    }
}