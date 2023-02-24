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

            Admins = new AdminRepository(appDbContext);

            ExtraLessonDetails = new ExtraLessonDetailsRepository(appDbContext);
            ExtraLessons = new ExtraLessonRepository(appDbContext);

            Students = new StudentRepository(appDbContext);
            StudentSubjects = new StudentSubjectRepository(appDbContext);

            Subjects = new SubjectRepository(appDbContext);

            Teachers = new TeacherRepository(appDbContext);
        }
        
        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
