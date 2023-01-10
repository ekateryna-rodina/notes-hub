using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Domain.Models.User;
using MediatR;

namespace LightNote.Application.BusinessLogic.Friends.Queries
{
    public class GetFriendshipDetails : IRequest<IEnumerable<UserProfile>>
    {
        public Guid UserId { get; set; }
    }
}