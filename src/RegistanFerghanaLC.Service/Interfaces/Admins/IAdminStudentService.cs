using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;

namespace RegistanFerghanaLC.Service.Interfaces.Admins;
public interface IAdminStudentService
{
    public Task<bool> RegisterStudentAsync(StudentRegisterDto sdtudentRegisterDto);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> UpdateAsync(int id, StudentAllUpdateDto studentAllUpdateDto);
    public Task<PagedList<StudentBaseViewModel>> GetAllAsync(PaginationParams @params);
    public Task<StudentViewModel> GetByIdAsync(int id);
    public Task<PagedList<StudentBaseViewModel>> GetByNameAsync(PaginationParams @params, string name);
}
