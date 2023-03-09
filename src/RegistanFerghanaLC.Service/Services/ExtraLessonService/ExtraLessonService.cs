using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities.ExtraLessons;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Interfaces.ExtraLesson;
using RegistanFerghanaLC.Service.ViewModels.ExtraLessonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace RegistanFerghanaLC.Service.Services.ExtraLessonService
{
    public class ExtraLessonService : IExtraLessonService
    {
        private readonly IUnitOfWork _repository;

        public ExtraLessonService(IUnitOfWork unitOfWork)
        {
            this._repository = unitOfWork;
        }
        public Task<PagedList<ExtraLessonViewModel>> GetAllByDateAsync(int teacherId, PaginationParams @params)
        {
            var query = (from ExtraLesson in _repository.ExtraLessons.GetAll().Where(x => x.TeacherId == teacherId && x.StartTime >= DateTime.Now)
                        join student in _repository.Students.GetAll()
                        on ExtraLesson.StudentId equals student.Id
                        join teacher in _repository.Teachers.GetAll()
                        on teacherId equals teacher.Id


                        select new ExtraLessonViewModel()
                        {
                            Id = ExtraLesson.Id,
                            SubjectName = teacher.Subject,
                            TeacherName = teacher.FirstName,
                            StudentName = student.FirstName,
                            StartTime = ExtraLesson.StartTime,
                            EndTime = ExtraLesson.EndTime,

                        }).OrderBy( x => x.StartTime);
            return PagedList<ExtraLessonViewModel>.ToPagedListAsync(query, @params);

        }
    }
}
