using RegistanFerghanaLC.DataAccess.DbContexts;
using RegistanFerghanaLC.DataAccess.Interfaces;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Domain.Entities.Students;

namespace RegistanFerghanaLC.DataAccess.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
