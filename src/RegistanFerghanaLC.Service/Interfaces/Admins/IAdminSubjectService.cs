using RegistanFerghanaLC.Service.ViewModels.SubjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Interfaces.Admins
{
    public interface IAdminSubjectService
    {
        public Task<bool> SubjectCreateAsync(string subject);
        public Task<bool> DeleteSubjectAsync(int subjectId);
        public IEnumerable<SubjectViewModel> GetAllAsync();
    }
}
