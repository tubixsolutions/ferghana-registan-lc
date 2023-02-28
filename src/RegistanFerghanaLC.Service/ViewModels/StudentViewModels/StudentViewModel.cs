using RegistanFerghanaLC.Domain.Entities;
using RegistanFerghanaLC.Domain.Enums;

namespace RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
public class StudentViewModel: StudentBaseViewModel
{
    public List<Subject>? Subjects { get; set; }
    public byte WeeklyLimit { get; set; }
    public EnglishLevel StudentLevel { get; set; }
    public DateTime CreatedAt { get; set; }

}
