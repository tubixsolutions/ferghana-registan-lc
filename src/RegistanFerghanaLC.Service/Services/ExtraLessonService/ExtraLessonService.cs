using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities.ExtraLessons;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.ExtraLesson;
using RegistanFerghanaLC.Service.Interfaces.ExtraLesson;
using RegistanFerghanaLC.Service.ViewModels.ExtraLessonViewModels;
using System.Net;

namespace RegistanFerghanaLC.Service.Services.ExtraLessonService
{
    public class ExtraLessonService : IExtraLessonService
    {
        private readonly IUnitOfWork _repository;
        private readonly IExtraLessonDetailsService _lessonDetailsService;
        public ExtraLessonService(IUnitOfWork unitOfWork, IExtraLessonDetailsService lessonDetailsService)
        {
            this._repository = unitOfWork;
            this._lessonDetailsService = lessonDetailsService;
        }

        public async Task<bool> CreateAsync(ExtraLessonCreateDto extraLesson)
        {
            var lesson = await _repository.ExtraLessons.FirstOrDefault(x => x.StartTime == DateTime.Parse(extraLesson.StartTime) && x.TeacherId == extraLesson.TeacherId);
            if (lesson == null)
            {
                var entity = new ExtraLesson()
                {
                    LessonTopic = extraLesson.LessonTopic,
                    StudentId = extraLesson.StudentId,
                    TeacherId = extraLesson.TeacherId,
                    SubjectId = extraLesson.SubjectId,
                    CreatedAt = TimeHelper.GetCurrentServerTime(),
                    LastUpdatedAt = TimeHelper.GetCurrentServerTime(),
                    StartTime = DateTime.Parse(extraLesson.StartTime),
                    EndTime = DateTime.Parse(extraLesson.StartTime).AddMinutes(20),
                };
                var res = _repository.ExtraLessons.Add(entity);
                var result = await _repository.SaveChangesAsync();
                if (result > 0)
                {
                    var resultDetails = await _lessonDetailsService.CreateDefaultAsync(res.Id);
                    return resultDetails ? true : false;
                }
                else return false;

            }
            else throw new StatusCodeException(HttpStatusCode.BadRequest, "This time is busy !");
        }

        public Task<PagedList<ExtraLessonViewModel>> GetAllByDateAsync(int teacherId, PaginationParams @params)
        {
            var query = (from ExtraLesson in _repository.ExtraLessons.GetAll().Where(x => x.TeacherId == teacherId && x.StartTime >= TimeHelper.GetCurrentServerTime())
                         join student in _repository.Students.GetAll()
                         on ExtraLesson.StudentId equals student.Id
                         join teacher in _repository.Teachers.GetAll()
                         on teacherId equals teacher.Id
                         select new ExtraLessonViewModel()
                         {
                             Id = ExtraLesson.Id,
                             LessonTopic = ExtraLesson.LessonTopic,
                             SubjectName = teacher.Subject,
                             TeacherName = teacher.FirstName,
                             StudentName = student.FirstName,
                             StartTime = ExtraLesson.StartTime,
                             EndTime = ExtraLesson.EndTime,
                         }).OrderBy(x => x.StartTime);
            return PagedList<ExtraLessonViewModel>.ToPagedListAsync(query, @params);
        }
    }
}
