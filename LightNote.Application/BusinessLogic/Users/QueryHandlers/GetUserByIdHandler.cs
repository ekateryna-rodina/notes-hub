using System;
using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Dal;
using LightNote.Domain.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.BusinessLogic.Users.QueryHandlers
{
	public class GetUserByIdHandler : IRequestHandler<GetUserById, UserProfile>
	{
        private readonly AppDbContext _context;
		public GetUserByIdHandler(AppDbContext context)
		{
            _context = context;
		}

        public async Task<UserProfile> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(u => u.Id == request.Id);
        }
    }
}

