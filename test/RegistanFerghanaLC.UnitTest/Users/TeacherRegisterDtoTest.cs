using RegistanFerghanaLC.Service.Dtos.Teachers;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.UnitTest.Users;

public class TeacherRegisterDtoTest
{
    [Theory]
    [InlineData("Fazliddin", "Mustafoyev", "09/21/2001", "middle", true, "21O92oo1", "+998932313101", "subject")]
    [InlineData("Qahhor", "Xurramov", "04/12/2002", "senior", false, "Qa123456", "+998933242002", "physics")]
    [InlineData("Normadjon", "G'offorov", "03/13/2003", "senior", false, "Nn123456", "+998901230011", "IT")]

    public void TeacherRegisterReturnTrue(string name, string lastname, DateTime time, string level,
        bool days, string password, string number, string subject)
    {
        TeacherRegisterDto dto = new()
        {
            FirstName = name,
            LastName = lastname,
            BirthDate = time,
            TeacherLevel = level,
            PartOfDay = Domain.Enums.PartOfDay.FirstPartOfDay,
            WorkDays = days,
            Password = password,
            PhoneNumber = number,
            Subject = subject
        };
        try
        {
            Validator.ValidateObject(dto, new ValidationContext(dto), true);
            Assert.True(true);
        }
        catch (Exception ex)
        {

            Assert.Fail(ex.Message);
        }
    }
    [Theory]
    [InlineData("Fazliddin", "Mustafoyev", "09/21-2001", "middle", true, "21O92oo1", "+998932313101", "subject")]
    [InlineData("Qahhor", "Xurramov", "04/12/2002", "lvel", false, "Q123456", "+998963242002", "physics")]
    [InlineData("Normadjon", "G'offorov", "03/13/2003", "senior", false, "Nn123456", "+9901230011", "IT")]
    public void TeacherRegisterReturnFalse(string name, string lastname, DateTime time, string level,
        bool days, string password, string number, string subject)
    {
        TeacherRegisterDto dto = new()
        {
            FirstName = name,
            LastName = lastname,
            BirthDate = time,
            TeacherLevel = level,
            PartOfDay = Domain.Enums.PartOfDay.FirstPartOfDay,
            WorkDays = days,
            Password = password,
            PhoneNumber = number,
            Subject = subject
        };
        try
        {
            Validator.ValidateObject(dto, new ValidationContext(dto), false);
            Assert.False(false);
        }
        catch (Exception ex)
        {

            Assert.Fail(ex.Message);
        }
    }
}
