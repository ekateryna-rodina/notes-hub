using System;
using LightNote.Application.BusinessLogic.Friends.Queries;
using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Dal;
using LightNote.Domain.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.BusinessLogic.Friends.QueryHandlers
{
    public class GetFollowersHandler : IRequestHandler<GetFriendshipDetails, IEnumerable<UserProfile>>
    {
        private readonly AppDbContext _context;
        public GetFollowersHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProfile>> Handle(GetFriendshipDetails request, CancellationToken cancellationToken)
        {
            // TODO: check user id
            var user = _context.UserProfiles.FirstOrDefault(u => u.Id == request.UserId);
            // TODO: user not found error!
            return await user.Subscription.Followers.AsQueryable().ToListAsync();
        }
    }
}

