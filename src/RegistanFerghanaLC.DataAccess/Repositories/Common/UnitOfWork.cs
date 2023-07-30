using Microsoft.EntityFrameworkCore.ChangeTracking;
using RegistanFerghanaLC.DataAccess.DbContexts;
using RegistanFerghanaLC.DataAccess.Interfaces;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;

namespace RegistanFerghanaLC.DataAccess.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public IAdminRepository Admins { get; }
        public IExtraLessonDetailsRepository ExtraLessonDetails { get; }
        public IExtraLessonRepository ExtraLessons { get; }
        public IStudentRepository Students { get; }
        public IStudentSubjectRepository StudentSubjects { get; }
        public ISubjectRepository Subjects { get; }
        public ITeacherRepository Teachers { get; }


        public UnitOfWork(AppDbContext appDbContext)
        {
            this.dbContext = appDbContext;

            Admins = new AdminRepository(dbContext);

            ExtraLessonDetails = new ExtraLessonDetailsRepository(dbContext);
            ExtraLessons = new ExtraLessonRepository(dbContext);

            Students = new StudentRepository(dbContext);

            StudentSubjects = new StudentSubjectRepository(dbContext);

            Subjects = new SubjectRepository(dbContext);

            Teachers = new TeacherRepository(dbContext);
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return dbContext.Entry(entity);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
