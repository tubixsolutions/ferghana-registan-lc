using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Teachers;

public class TeacherRegisterDto: AccountRegisterDto
{
    [Required(ErrorMessage = "Please select the subject of the teacher!")]
    public string Subject { get; set; } = String.Empty;
    public string TeacherLevel { get; set; } = String.Empty;
    public PartOfDay PartOfDay { get; set; }
    public bool WorkDays { get; set; } = true;

    public static implicit operator Teacher(TeacherRegisterDto teacherRegisterDto)
    {
        return new Teacher()
        {
            FirstName= teacherRegisterDto.FirstName,
            LastName= teacherRegisterDto.LastName,
            PhoneNumber= teacherRegisterDto.PhoneNumber,
            CreatedAt = TimeHelper.GetCurrentServerTime(),
            LastUpdatedAt= TimeHelper.GetCurrentServerTime(),
            Subject= teacherRegisterDto.Subject,
            TeacherLevel = teacherRegisterDto.TeacherLevel,
            PartOfDay= teacherRegisterDto.PartOfDay,
            WorkDays= teacherRegisterDto.WorkDays,
            BirthDate = teacherRegisterDto.BirthDate
        };
    }
}
