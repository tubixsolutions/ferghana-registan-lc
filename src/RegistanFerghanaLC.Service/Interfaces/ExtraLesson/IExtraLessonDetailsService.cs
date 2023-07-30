using RegistanFerghanaLC.Service.Dtos.ExtraLesson;

namespace RegistanFerghanaLC.Service.Interfaces.ExtraLesson
{
    public interface IExtraLessonDetailsService
    {
        public Task<bool> CreateDefaultAsync(int extraLessonId);
        public Task<bool> UpdateAsync(int extraLessonId, ExtraLessonDetailsUpdateDto updateDto);
    }
}
