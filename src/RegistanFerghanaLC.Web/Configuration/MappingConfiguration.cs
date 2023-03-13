using AutoMapper;
using RegistanFerghanaLC.Domain.Entities;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Domain.Entities.Users;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Admins;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Subjects;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.ViewModels.StudentSubjectViewModels;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;
using RegistanFerghanaLC.Service.ViewModels.SubjectViewModels;

namespace RegistanFerghanaLC.Web.Configuration;
public class MappingConfiguration: Profile
{
	public MappingConfiguration()
	{
		CreateMap<AdminRegisterDto, Admin>().ReverseMap();
		CreateMap<StudentBaseViewModel, Student>().ReverseMap();
        CreateMap<TeacherViewDto, Teacher>().ReverseMap();
		CreateMap<StudentViewModel, Student>().ReverseMap();
		CreateMap<StudentAllUpdateDto, Student>().ReverseMap();
		CreateMap<SubjectViewModel, Subject>().ReverseMap();
		CreateMap<StudentSubjectViewModel, StudentSubject>().ReverseMap();
		CreateMap<StudentRegisterDto, Student>().ReverseMap();
		CreateMap<SubjectCreateDto, Subject>().ReverseMap();
	}
}
