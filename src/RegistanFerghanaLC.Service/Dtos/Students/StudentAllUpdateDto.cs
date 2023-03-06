using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Students;
public class StudentAllUpdateDto: AccountRegisterDto
{
    [Required(ErrorMessage = "Please select a subject.")]
    public string Subject { get; set; } = string.Empty;
    public EnglishLevel StudentLevel { get; set; }

    public static implicit operator Student(StudentAllUpdateDto studentAllUpdateDto)
    {
        return new Student()
        {
            FirstName = studentAllUpdateDto.FirstName,
            LastName = studentAllUpdateDto.LastName,
            PhoneNumber = studentAllUpdateDto.PhoneNumber,
            BirthDate = studentAllUpdateDto.BirthDate,
            CreatedAt = TimeHelper.GetCurrentServerTime(),
            LastUpdatedAt = TimeHelper.GetCurrentServerTime(),
            StudentLevel = studentAllUpdateDto.StudentLevel,
        };
    }
}
