using RegistanFerghanaLC.Service.Dtos.Students;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.UnitTest.Users;

public class StudentRegisterDtoTest
{
    [Theory]
    [InlineData("Fazliddin", "Mustafoyev", "09/21/2001", "21O92oo1", "+998932313101", "english")]
    [InlineData("Qahhor", "Xurramov", "04/12/2002", "Qa123456", "+998933242002", "physics")]
    [InlineData("Normadjon", "G'offorov", "03/13/2003", "Nn123456", "+998901230011", "IT")]

    public void StudentRegisterReturnTrue(string name, string lastname, DateTime time,
         string password, string number, string subject)
    {
        StudentRegisterDto dto = new()
        {
            FirstName = name,
            LastName = lastname,
            BirthDate = time,
            StudentLevel = Domain.Enums.EnglishLevel.Elementary,
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
    [InlineData("Fazliddin", "Mustafoyev", "09/21-2001", "21O92oo1", "+998932313101", "subject")]
    [InlineData("Qahhor", "Xurramov", "04/12/2002", "Q123456", "+998963242002", "physics")]
    [InlineData("Normadjon", "G'offorov", "03/13/2003", "Nn123456", "+9901230011", "IT")]
    public void StudentRegisterReturnFalse(string name, string lastname, DateTime time,
        string password, string number, string subject)
    {
        StudentRegisterDto dto = new()
        {
            FirstName = name,
            LastName = lastname,
            BirthDate = time,
            StudentLevel = Domain.Enums.EnglishLevel.Beginner,
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
