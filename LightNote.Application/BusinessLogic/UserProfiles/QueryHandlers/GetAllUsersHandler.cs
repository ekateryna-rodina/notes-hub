using LightNote.Application.BusinessLogic.UserProfiles.Queries;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.UserProfileAggregate;
using MediatR;

namespace LightNote.Application.BusinessLogic.UserProfiles.QueryHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, IEnumerable<UserProfile>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserProfile>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.Get();
        }
    }
}

