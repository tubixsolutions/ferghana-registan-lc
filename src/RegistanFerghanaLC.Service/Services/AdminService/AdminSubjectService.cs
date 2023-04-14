using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.DataAccess.Interfaces.Common;
using RegistanFerghanaLC.Domain.Entities;
using RegistanFerghanaLC.Service.Common.Exceptions;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Interfaces.Admins;
using RegistanFerghanaLC.Service.ViewModels.SubjectViewModels;

namespace RegistanFerghanaLC.Service.Services.AdminService
{
    public class AdminSubjectService : IAdminSubjectService
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public AdminSubjectService(IUnitOfWork repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        public async Task<bool> SubjectCreateAsync(string subject)
        {
            var check = await _repository.Subjects.GetAll().FirstOrDefaultAsync(x => x.Name == subject);
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
        public async Task<bool> DeleteSubjectAsync(int subjectId)
        {
            var subject = _repository.Subjects.FindByIdAsync(subjectId);
            if (subject == null)
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, "Subject not found");
            }
            _repository.Subjects.Delete(subjectId);

            var res = await _repository.SaveChangesAsync();
            return res > 0;
        }

        public IEnumerable<SubjectViewModel> GetAllAsync()
        {
            var subjects = _repository.Subjects.GetAll();
            var res = subjects.Select(s => _mapper.Map<SubjectViewModel>(s));
            return res;
        }
    }
}
