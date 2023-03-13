using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Common.Attributes;
[AttributeUsage(AttributeTargets.Property)]
public class RankAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Enter the time");
        else
        {
            var rank = int.Parse(value.ToString()!);
            if (rank > 5) return new ValidationResult("The value should not be greater than 5");
            if (rank < 0) return new ValidationResult("The value must not be less than 0");
            return ValidationResult.Success;
        }
    }
}
