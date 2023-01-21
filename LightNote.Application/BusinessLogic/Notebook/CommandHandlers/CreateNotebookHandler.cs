using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.Notebook.Commands;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.CommandHandlers
{
    public class CreateNotebookHandler : IRequestHandler<CreateNotebook, OperationResult<LightNote.Domain.Models.NotebookAggregate.Notebook>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNotebookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Domain.Models.NotebookAggregate.Notebook>> Handle(CreateNotebook request, CancellationToken cancellationToken)
        {
            var notebook = LightNote.Domain.Models.NotebookAggregate.Notebook.Create(request.Title, request.UserProfileId);
            try
            {
                _unitOfWork.NotebookRepository.Insert(notebook);
                await _unitOfWork.SaveAsync();
                return OperationResult<Domain.Models.NotebookAggregate.Notebook>.CreateSuccess(notebook);
            }
            catch (Exception ex)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Notebook>.CreateFailure(new[] { ex });
            }
        }
    }
}