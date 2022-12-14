using System;
using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Dal;
using LightNote.Domain.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.BusinessLogic.Users.QueryHandlers
{
	public class GetFollowingHandler : IRequestHandler<GetSubscriptionDetails, IEnumerable<UserProfile>>
	{
        private readonly AppDbContext _context;
        public GetFollowingHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProfile>> Handle(GetSubscriptionDetails request, CancellationToken cancellationToken)
        {
            // TODO: check user id
            var user = _context.UserProfiles.FirstOrDefault(u => u.Id == request.UserId);
            // TODO: user not found error!
            return await user.Subscription.Following.AsQueryable().ToListAsync();
        }
    }
}

