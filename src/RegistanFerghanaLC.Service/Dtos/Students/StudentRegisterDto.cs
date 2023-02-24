using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Dtos.Accounts;

namespace RegistanFerghanaLC.Service.Dtos.Students;
public class StudentRegisterDto: AccountRegisterDto
{
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
