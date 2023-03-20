using Microsoft.AspNetCore.Http;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Common.Attributes;
using RegistanFerghanaLC.Service.Common.Helpers;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using System.ComponentModel.DataAnnotations;

namespace RegistanFerghanaLC.Service.Dtos.Students;
public class StudentAllUpdateDto: AccountRegisterDto
{
    [AllowedFiles(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" }), MaxFile(2)]
    public IFormFile? Image { get; set; }

    public string ImagePath { get; set; }
    
    [Required(ErrorMessage = "Please select a subject.")]
    public EnglishLevel StudentLevel { get; set; }
    
    public string Subject { get; set; } = String.Empty;

    public static implicit operator Student(StudentAllUpdateDto studentAllUpdateDto)
    {
        return new Student()
        {
            Id = studentAllUpdateDto.Id,
            FirstName = studentAllUpdateDto.FirstName,
            LastName = studentAllUpdateDto.LastName,
            PhoneNumber = studentAllUpdateDto.PhoneNumber,
            BirthDate = studentAllUpdateDto.BirthDate,
            CreatedAt = TimeHelper.GetCurrentServerTime(),
            LastUpdatedAt = TimeHelper.GetCurrentServerTime(),
            StudentLevel = studentAllUpdateDto.StudentLevel,
            Image = studentAllUpdateDto.Image.ToString(),
        };
    }
}
