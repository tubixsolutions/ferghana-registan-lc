using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RegistanFerghanaLC.DataAccess.Interfaces.Common;
public interface IUnitOfWork
{
    public IAdminRepository Admins { get; }
    public IExtraLessonDetailsRepository ExtraLessonDetails { get; }
    public IExtraLessonRepository ExtraLessons { get; }
    public IStudentRepository Students { get; }
    public IStudentSubjectRepository StudentSubjects { get; }
    public ISubjectRepository Subjects { get; }
    public ITeacherRepository Teachers { get; }
    public Task<int> SaveChangesAsync();
    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

}
