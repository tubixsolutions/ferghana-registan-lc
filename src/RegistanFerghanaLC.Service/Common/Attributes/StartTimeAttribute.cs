using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Common.Attributes;
[AttributeUsage(AttributeTargets.Property)]
public class StartTimeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Enter the time");
        else
        {
            var date = (string)value;
            if (DateTime.Parse(date) > DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else return new ValidationResult("Enter the correct time");
        }
    }
}
