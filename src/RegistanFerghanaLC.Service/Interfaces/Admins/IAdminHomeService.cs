using RegistanFerghanaLC.Service.ViewModels.TeacherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Interfaces.Admins
{
    public interface IAdminHomeService
    {
        public Task<List<TeacherByLessonsNumberViewModel>> GetTopTeachersAsync();
        public Task<List<TeacherByLessonsNumberViewModel>> GetTopTeachersByRankAsync();
    }
}