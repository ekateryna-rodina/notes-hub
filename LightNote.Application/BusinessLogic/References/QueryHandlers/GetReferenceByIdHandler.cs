using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.References.Queries;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.QueryHandlers
{
    public class GetReferenceByIdHandler : IRequestHandler<GetReferenceById, OperationResult<Reference?>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReferenceByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Reference?>> Handle(GetReferenceById request, CancellationToken cancellationToken)
        {
            var reference = await _unitOfWork.ReferenceRepository.GetByID(request.Id);
            if (reference != null && reference.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<Reference?>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            return OperationResult<Reference?>.CreateSuccess(reference);
        }
    }
}