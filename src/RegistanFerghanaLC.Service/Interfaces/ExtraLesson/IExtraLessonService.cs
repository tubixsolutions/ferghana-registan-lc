using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.ExtraLesson;
using RegistanFerghanaLC.Service.ViewModels.ExtraLessonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Interfaces.ExtraLesson
{
    public interface IExtraLessonService
    {
        public Task<PagedList<ExtraLessonViewModel>> GetAllByDateAsync(int teacherId, PaginationParams @params);
        public Task<bool> CreateAsync(ExtraLessonCreateDto extraLesson);
    }
}
