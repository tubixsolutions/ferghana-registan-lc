using RegistanFerghanaLC.DataAccess.DbContexts;
using RegistanFerghanaLC.DataAccess.Interfaces;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Domain.Entities;

namespace RegistanFerghanaLC.DataAccess.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
