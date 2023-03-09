using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities.ExtraLessons;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Interfaces.ExtraLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Services.ExtraLessonService
{
    public class ExtraLessonDetailsService : IExtraLessonDetailsService
    {
        private readonly IUnitOfWork _repository;
        public ExtraLessonDetailsService(IUnitOfWork repository)
        {
            this._repository = repository;
        }
        public async Task<bool> CreateDefaultAsync(int extraLessonId)
        {
            var check = await _repository.ExtraLessonDetails.FirstOrDefault(x => x.ExtraLessonId == extraLessonId);
            if (check == null)
            {
                var entity = new ExtraLessonDetails()
                {
                    ExtraLessonId = extraLessonId,
                    IsDone = false,
                    Rank = 2,
                    Comment = string.Empty,
                    CreatedAt = DateTime.Now,
                    LastUpdatedAt = DateTime.Now
                };
                var res = _repository.ExtraLessonDetails.Add(entity);

                var result = await _repository.SaveChangesAsync();

                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.BadRequest, "This details for Extra Lesson are already exist !");
        }
    }
}
