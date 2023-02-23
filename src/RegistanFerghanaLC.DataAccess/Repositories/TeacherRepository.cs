using RegistanFerghanaLC.DataAccess.DbContexts;
using RegistanFerghanaLC.DataAccess.Interfaces;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Domain.Entities.Teachers;

namespace RegistanFerghanaLC.DataAccess.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
