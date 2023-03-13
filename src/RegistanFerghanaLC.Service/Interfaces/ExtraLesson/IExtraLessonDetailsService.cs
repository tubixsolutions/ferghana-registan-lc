using RegistanFerghanaLC.Domain.Entities.ExtraLessons;
using RegistanFerghanaLC.Service.Dtos.ExtraLesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Interfaces.ExtraLesson
{
    public interface IExtraLessonDetailsService
    {
        public Task<bool> CreateDefaultAsync(int extraLessonId);
        public Task<bool> UpdateAsync(int extraLessonId, ExtraLessonDetailsUpdateDto updateDto);
    }
}
