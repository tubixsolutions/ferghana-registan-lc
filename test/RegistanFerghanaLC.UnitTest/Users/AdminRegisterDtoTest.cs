using RegistanFerghanaLC.Service.Dtos.Admins;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.UnitTest.Users
{
    public class AdminRegisterDtoTest
    {
        [Theory]
        [InlineData("Fazliddin", "Mustafoyev", "09/21/2001", "Tashkent",  "21O92oo1", "+998932313101")]
        [InlineData("Qahhor", "Xurramov", "04/12/2002", "Termiz",  "Qa123456", "+998933242002")]
        [InlineData("Normadjon", "G'offorov", "03/13/2003", "Farghana", "Nn123456", "+998901230011")]

        public void TeacherRegisterReturnTrue(string name, string lastname, DateTime time, string adress,
                                               string password, string number)
        {
            AdminRegisterDto dto = new()
            {
                FirstName = name,
                LastName = lastname,
                BirthDate = time,
                Address = adress,
                Password = password,
                PhoneNumber = number
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
        [InlineData("Fazliddin", "Mustafoyev", "09/21-2001", "Namangan", "21O92oo1", "+998932313101")]
        [InlineData("Qahhor", "Xurramov", "04/12/2002", "Jizzax", "Q123456", "+998963242002 ")]
        [InlineData("Normadjon", "G'offorov", "03/13/2003", "Samarkand", "Nn123456", "+9901230011")]
        public void TeacherRegisterReturnFalse(string name, string lastname, DateTime time, string adress,
             string password, string number)
        {
            AdminRegisterDto dto = new()
            {
                FirstName = name,
                LastName = lastname,
                BirthDate = time,
                Address = adress,
                Password = password,
                PhoneNumber = number,
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
}
