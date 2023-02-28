using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Students;
public class StudentRegisterDto: AccountRegisterDto
{
    [Required(ErrorMessage = "Please select a subject.")]
    public string Subject { get; set; } = string.Empty;
    public EnglishLevel StudentLevel { get; set; }

    public static implicit operator Student(StudentRegisterDto studentRegisterDto)
    {
        return new Student()
        {
            FirstName= studentRegisterDto.FirstName,
            LastName= studentRegisterDto.LastName,
            PhoneNumber= studentRegisterDto.PhoneNumber,
            BirthDate= studentRegisterDto.BirthDate,
            CreatedAt = TimeHelper.GetCurrentServerTime(),
            LastUpdatedAt= TimeHelper.GetCurrentServerTime(),
            StudentLevel= studentRegisterDto.StudentLevel,
        };
    }

}
