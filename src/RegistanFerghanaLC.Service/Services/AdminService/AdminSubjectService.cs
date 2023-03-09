using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Services.AdminService
{
    public class AdminSubjectService : IAdminSubjectService
    {
        private readonly IUnitOfWork _repository;
        public AdminSubjectService(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task<bool> SubjectCreateAsync(string subject)
        {
            var check = _repository.Subjects.Where(x => x.Name.ToLower()== subject.ToLower()).ToList();
            if(check is not null)
            {
                throw new AlreadyExistingException(nameof(subject), "The subject is already registered");
            }
            var newsubject = new Subject()
            {
                Name= subject,
                CreatedAt= TimeHelper.GetCurrentServerTime(),
                LastUpdatedAt= TimeHelper.GetCurrentServerTime()
            };
            _repository.Subjects.Add(newsubject);
            var res = await _repository.SaveChangesAsync();
            return res > 0;
        }
    }
}
