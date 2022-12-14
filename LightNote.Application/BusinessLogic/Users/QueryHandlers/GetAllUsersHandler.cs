using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.BusinessLogic.Users.QueryHandlers
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

