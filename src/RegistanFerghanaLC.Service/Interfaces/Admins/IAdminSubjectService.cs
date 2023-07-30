using RegistanFerghanaLC.Service.ViewModels.SubjectViewModels;

namespace RegistanFerghanaLC.Service.Interfaces.Admins
{
    public interface IAdminSubjectService
    {
        public Task<bool> SubjectCreateAsync(string subject);
        public Task<bool> DeleteSubjectAsync(int subjectId);
        public IEnumerable<SubjectViewModel> GetAllAsync();
    }
}
