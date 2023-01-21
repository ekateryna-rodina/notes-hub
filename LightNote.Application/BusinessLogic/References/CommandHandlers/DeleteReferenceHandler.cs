using System;
using LightNote.Application.BusinessLogic.References.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.References.CommandHandlers
{
	public class DeleteReferenceHandler : IRequestHandler<DeleteReference, OperationResult<bool>>
	{
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReferenceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteReference request, CancellationToken cancellationToken)
        {
            var reference = await _unitOfWork.ReferenceRepository.GetById(ReferenceId.Create(request.Id));
            if (reference == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(Reference)) });
            }
            if (reference.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            try {
                _unitOfWork.ReferenceRepository.Delete(reference);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex) {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }
            return OperationResult<bool>.CreateSuccess(true);
        }
    }
}

