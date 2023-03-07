using RegistanFerghanaLC.Service.Common.Utils;
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
        public Task<PagedList<SalaryBaseViewModel>> GetAllAsync(PaginationParams @params);
        public Task<PagedList<SalaryBaseViewModel>> GetAllByDateAsync(PaginationParams @params, DateTime startDate, DateTime endDate);
        public Task<PagedList<SalaryViewModel>> GetAllByIdAsync(int id, PaginationParams @params, DateTime startDate, DateTime endDate);
    }
}
