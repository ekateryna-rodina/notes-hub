using System;
using LightNote.Application.Users.Queries;
using LightNote.Dal;
using LightNote.Domain.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.Users.QueryHandlers
{
	public class GetAllUsersHandler : IRequestHandler<GetAllUsers, IEnumerable<UserProfile>>
	{
		private readonly AppDbContext _context;
		public GetAllUsersHandler(AppDbContext context)
		{
			_context = context;
		}

        public async Task<IEnumerable<UserProfile>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
			// TODO: get all users by userId?
			return await _context.UserProfiles.ToListAsync();
		}
	}
}

