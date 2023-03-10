
using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Service.ViewModels.StudentSubjectViewModels;

namespace RegistanFerghanaLC.Service.Interfaces.Common;
public interface IStudentSubjectService
{
    public Task<bool> SaveStudentSubjectAsync(int studentId, int subjectId);
    public Task<bool> DeleteStudentSubjectAsync(int studentSubjectId);
    public Task<IEnumerable<StudentSubjectViewModel>> GetStudentSubjectAsync(int studentId);
}
