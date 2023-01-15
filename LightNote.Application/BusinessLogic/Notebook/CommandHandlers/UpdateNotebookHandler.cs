using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.Notebook.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.CommandHandlers
{
    public class UpdateNotebookHandler : IRequestHandler<UpdateNotebook, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateNotebookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(UpdateNotebook request, CancellationToken cancellationToken)
        {
            var notebook = await _unitOfWork.NotebookRepository.GetByID(request.Id);
            if (notebook.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            try
            {
                notebook.UpdateTitle(request.Title);
                _unitOfWork.NotebookRepository.Update(notebook);
                await _unitOfWork.SaveAsync();
                return OperationResult<bool>.CreateSuccess(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }
        }
    }
}