using RegistanFerghanaLC.Service.Dtos.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RegistanFerghanaLC.UnitTest.Users;

public class UserLoginDtosTest
{
    [Theory]
    [InlineData("+998932313101","Nn123456")]
    [InlineData("+998914722002","Qq123456")]
    [InlineData("+998941234567", "Qa654321")]
    public void LoginDtoReturnTrue(string number, string password)
    {
        AccountLoginDto dto = new ()
        {
         Password = password,
         PhoneNumber= number
        };
        try
        {
            Validator.ValidateObject(dto, new ValidationContext(dto), true);
            Assert.True(true);
        }
        catch(Exception ex)  
        {

            Assert.Fail(ex.Message);
        }
    }
    [Theory]
    [InlineData("+9989213121", "12345678")]
    [InlineData("+00000000000","A123456A")]
    [InlineData("+123432441","a123456")]
    public void LoginDtoReturn(string number, string password)
    {
        AccountLoginDto dto = new()
        {
            Password = password,
            PhoneNumber = number
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
