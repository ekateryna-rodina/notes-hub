using LightNote.Domain.Models.UserProfileAggregate;
using MediatR;

namespace LightNote.Application.BusinessLogic.UserProfiles.Queries
{
    public class GetAllUsers : IRequest<IEnumerable<UserProfile>>
    {

    }
}

