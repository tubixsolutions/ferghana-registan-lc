using AutoMapper;
using RegistanFerghanaLC.Domain.Entities.Users;
using RegistanFerghanaLC.Service.Dtos.Accounts;
using RegistanFerghanaLC.Service.Dtos.Admins;

namespace RegistanFerghanaLC.Web.Configuration;
public class MappingConfiguration: Profile
{
	public MappingConfiguration()
	{
		CreateMap<AdminRegisterDto, Admin>();
	}
}
