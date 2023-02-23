using RegistanFerghanaLC.DataAccess.DbContexts;
using RegistanFerghanaLC.DataAccess.Interfaces;
using RegistanFerghanaLC.DataAccess.Repositories.Common;
using RegistanFerghanaLC.Domain.Entities.ExtraLessons;

namespace RegistanFerghanaLC.DataAccess.Repositories
{
    public class ExtraLessonRepository : GenericRepository<ExtraLesson>, IExtraLessonRepository
    {
        public ExtraLessonRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
