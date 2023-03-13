using RegistanFerghanaLC.Domain.Entities;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Service.ViewModels.StudentSubjectViewModels;
namespace RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
public class StudentBaseViewModel
{
    public int Id { get; set; }
    
    public string FirstName { get; set; } = String.Empty;
    
    public string LastName { get; set; } = String.Empty;
    
    public string PhoneNumber { get; set; } = String.Empty;
    public byte WeeklyLimit { get; set; }
    public IEnumerable<string>? Subjects { get; set; }
    public string? Image { get; set; }
}
