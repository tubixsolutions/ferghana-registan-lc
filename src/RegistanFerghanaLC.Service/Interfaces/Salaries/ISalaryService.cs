using RegistanFerghanaLC.Service.ViewModels.SalaryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Interfaces.Salaries
{
    public interface ISalaryService
    {
        public Task<IEnumerable<SalaryBaseViewModel>> GetAllAsync();
    }
}
