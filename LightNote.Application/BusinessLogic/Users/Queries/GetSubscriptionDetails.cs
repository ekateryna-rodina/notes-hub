using System;
using LightNote.Domain.Models.User;
using MediatR;

namespace LightNote.Application.BusinessLogic.Users.Queries
{
	public class GetSubscriptionDetails : IRequest<IEnumerable<UserProfile>>
	{
		public Guid UserId { get; set; }
	}
}

