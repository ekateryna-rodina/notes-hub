using LightNote.Application.BusinessLogic.UserProfiles.Queries;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.BusinessLogic.UserProfiles.QueryHandlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, UserProfile>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfile> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRepository.GetByID(request.Id);
        }
    }
}

