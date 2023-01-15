using LightNote.Domain.Models.UserProfileAggregate;
using MediatR;

namespace LightNote.Application.BusinessLogic.UserProfiles.Queries
{
    public class GetUserById : IRequest<UserProfile>
    {
        public Guid Id { get; set; }
    }
}

