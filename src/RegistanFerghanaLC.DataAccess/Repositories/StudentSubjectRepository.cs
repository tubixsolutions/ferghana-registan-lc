using RegistanFerghanaLC.DataAccess.DbContexts;
using RegistanFerghanaLC.DataAccess.Interfaces;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Domain.Entities.Students;

namespace RegistanFerghanaLC.DataAccess.Repositories
{
    public class StudentSubjectRepository : GenericRepository<StudentSubject>, IStudentSubjectRepository
    {
        public StudentSubjectRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
