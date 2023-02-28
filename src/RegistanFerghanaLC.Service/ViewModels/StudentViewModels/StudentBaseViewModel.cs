namespace RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
public class StudentBaseViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string? Image { get; set; }
}
