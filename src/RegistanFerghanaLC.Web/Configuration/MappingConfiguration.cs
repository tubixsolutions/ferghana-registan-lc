using AutoMapper;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Entities.Users;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Admins;
using RegistanFerghanaLC.Service.ViewModels.StudentViewModels;

namespace RegistanFerghanaLC.Web.Configuration;
public class MappingConfiguration: Profile
{
	public MappingConfiguration()
	{
		CreateMap<AdminRegisterDto, Admin>().ReverseMap();
		CreateMap<StudentBaseViewModel, Student>().ReverseMap();
		CreateMap<StudentViewModel, Student>().ReverseMap();
	}
}
