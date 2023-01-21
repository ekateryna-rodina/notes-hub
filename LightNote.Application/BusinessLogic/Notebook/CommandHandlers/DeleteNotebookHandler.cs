using System;
using LightNote.Application.BusinessLogic.Notebook.Commands;
using LightNote.Application.BusinessLogic.References.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.CommandHandlers
{
	public class DeleteNotebookHandler : IRequestHandler<DeleteNotebook, OperationResult<bool>>
	{
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNotebookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(DeleteNotebook request, CancellationToken cancellationToken)
        {
            var notebook = await _unitOfWork.NotebookRepository.GetById(NotebookId.Create(request.Id));
            if (notebook == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(Notebook )) });
            }
            if (notebook.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            try
            {
                _unitOfWork.NotebookRepository.Delete(notebook);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }
            return OperationResult<bool>.CreateSuccess(true);
        }
    }
}

