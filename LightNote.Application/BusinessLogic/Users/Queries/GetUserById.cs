using System;
using LightNote.Domain.Models.User;
using MediatR;

namespace LightNote.Application.BusinessLogic.Users.Queries
{
	public class GetUserById : IRequest<UserProfile>
	{
		public Guid Id { get; set; }
	}
}

