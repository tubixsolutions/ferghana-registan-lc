using RegistanFerghanaLC.Domain.Common;

namespace RegistanFerghanaLC.Domain.Entities;
public class Human : Auditable
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string? Image { get; set; }

    public string PhoneNumber { get; set; } = String.Empty;

    public DateTime BirthDate { get; set; }
}
