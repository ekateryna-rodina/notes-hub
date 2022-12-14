using System;
using AutoMapper;
using LightNote.Api.Contracts.Identity.Request;
using LightNote.Application.BusinessLogic.Identity.Commands;

namespace LightNote.Api.MappingProfiles
{
	public class IdentityProfile : Profile
	{
		public IdentityProfile()
		{
			CreateMap<Registration, RegisterIdentity>();
		}
	}
}

