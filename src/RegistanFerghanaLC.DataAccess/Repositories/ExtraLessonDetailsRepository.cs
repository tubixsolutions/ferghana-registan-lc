using RegistanFerghanaLC.DataAccess.DbContexts;
using RegistanFerghanaLC.DataAccess.Interfaces;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Domain.Entities.ExtraLessons;

namespace RegistanFerghanaLC.DataAccess.Repositories
{
    public class ExtraLessonDetailsRepository : GenericRepository<ExtraLessonDetails>, IExtraLessonDetailsRepository
    {
        public ExtraLessonDetailsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

    }
}
