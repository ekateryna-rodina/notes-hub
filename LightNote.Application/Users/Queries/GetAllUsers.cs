using System;
using LightNote.Domain.Models.User;
using MediatR;

namespace LightNote.Application.Users.Queries
{
	public class GetAllUsers : IRequest<IEnumerable<UserProfile>>
	{
		
	}
}

