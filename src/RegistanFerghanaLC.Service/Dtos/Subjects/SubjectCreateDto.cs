
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Subjects;
public class SubjectCreateDto
{
    [Required(ErrorMessage = "Enter a subject name")]
    public string Name { get; set; } = String.Empty;

}
