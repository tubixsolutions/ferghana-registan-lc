using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.ViewModels.TeacherViewModels;

namespace RegistanFerghanaLC.Service.Services.AdminService
{
    public class AdminHomeService : IAdminHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminHomeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Task<List<TeacherByLessonsNumberViewModel>> GetTopTeachersByRankAsync()
        {
            var list = (from teachers in _unitOfWork.Teachers.GetAll()
                        let teacherExtraLessons = _unitOfWork.ExtraLessons.GetAll()
                           .Where(a => a.TeacherId == teachers.Id).ToList()
                        let lessonsNumber = teacherExtraLessons.Count()
                        let averageRank = (from extra in teacherExtraLessons
                                           join details in _unitOfWork.ExtraLessonDetails.GetAll()
                                           on extra.Id equals details.ExtraLessonId
                                           select details.Rank).ToList()
                        select new TeacherByLessonsNumberViewModel()
                        {
                            Id = teachers.Id,
                            FirstName = teachers.FirstName,
                            LastName = teachers.LastName,
                            Description = teachers.Description,
                            ImagePath = teachers.Image!,
                            PhoneNumber = teachers.PhoneNumber,
                            LessonsNumber = lessonsNumber,
                            AverageRank = averageRank.Count == 0 ? 0 : averageRank.Average()
                        }).Where(x => x.LessonsNumber > 0).OrderByDescending(x => x.AverageRank)
                        .ThenByDescending(x => x.LessonsNumber).Take(5).ToListAsync();
            return list;
        }

        public Task<List<TeacherByLessonsNumberViewModel>> GetTopTeachersAsync()
        {
            var list = (from teachers in _unitOfWork.Teachers.GetAll()
                        let teacherExtraLessons = _unitOfWork.ExtraLessons.GetAll()
                           .Where(a => a.TeacherId == teachers.Id).ToList()
                        let lessonsNumber = teacherExtraLessons.Count()
                        let averageRank = (from extra in teacherExtraLessons
                                           join details in _unitOfWork.ExtraLessonDetails.GetAll()
                                           on extra.Id equals details.ExtraLessonId
                                           select details.Rank).ToList()
                        select new TeacherByLessonsNumberViewModel()
                        {
                            Id = teachers.Id,
                            FirstName = teachers.FirstName,
                            LastName = teachers.LastName,
                            Description = teachers.Description,
                            ImagePath = teachers.Image!,
                            PhoneNumber = teachers.PhoneNumber,
                            LessonsNumber = lessonsNumber,
                            AverageRank = averageRank.Count == 0 ? 0 : averageRank.Average()
                        }).Where(x => x.LessonsNumber > 0).OrderByDescending(x => x.LessonsNumber)
                        .ThenByDescending(x => x.AverageRank).Take(5).ToListAsync();
            return list;
        }
    }
}
