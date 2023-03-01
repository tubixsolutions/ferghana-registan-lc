using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Interfaces.Salaries;
using RegistanFerghanaLC.Service.ViewModels.SalaryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Services.SalaryService
{
    public class SalaryService : ISalaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SalaryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<PagedList<SalaryBaseViewModel>> GetAllAsync(PaginationParams @params)
        {
            var query = (from salary in _unitOfWork.ExtraLessons.GetAll()
                         join teacher in _unitOfWork.Teachers.GetAll()
                         on salary.TeacherId equals teacher.Id
                         let lessonsNumber = _unitOfWork.ExtraLessons.GetAll().Where(a => a.TeacherId == teacher.Id).Count()
                         let averageRank = (from extra in _unitOfWork.ExtraLessons.GetAll()
                                            join details in _unitOfWork.ExtraLessonDetails.GetAll()
                                            on extra.Id equals details.ExtraLessonId
                                            select details.Rank).Average()
                         select new SalaryBaseViewModel()
                         {
                             Id = teacher.Id,
                             FirstName = teacher.FirstName,
                             LastName = teacher.LastName,
                             LessonsNumber = lessonsNumber,
                             AverageRank = averageRank
                         });
            return await PagedList<SalaryBaseViewModel>.ToPagedListAsync(query, @params);
        }

        public async Task<PagedList<SalaryViewModel>> GetAllByIdAsync(int id, PaginationParams @params)
        {
            var query = (from extra in _unitOfWork.ExtraLessons.GetAll()
                         join extraDetails in _unitOfWork.ExtraLessonDetails.GetAll()
                         on extra.Id equals extraDetails.ExtraLessonId
                         where extra.TeacherId == id
                         select new SalaryViewModel()
                         {
                             Id = extra.Id,
                             Rank = extraDetails.Rank,
                             Comment = extraDetails.Comment,
                             StartTime = extra.StartTime,
                             EndTime = extra.EndTime,
                         });
            return await PagedList<SalaryViewModel>.ToPagedListAsync(query, @params);
        }
    }
}
