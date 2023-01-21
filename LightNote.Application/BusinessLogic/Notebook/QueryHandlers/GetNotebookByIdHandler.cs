using LightNote.Application.BusinessLogic.Notebook.Queries;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.QueryHandlers
{
    public class GetNotebookByIdHandler : IRequestHandler<GetNotebookById, OperationResult<LightNote.Domain.Models.NotebookAggregate.Notebook?>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetNotebookByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Domain.Models.NotebookAggregate.Notebook?>> Handle(GetNotebookById request, CancellationToken cancellationToken)
        {
            try {
                var notebook = await _unitOfWork.NotebookRepository
                  .GetById(NotebookId.Create(request.Id));
                if (notebook != null && request.UserProfileId != notebook!.UserProfileId.Value)
                {
                    return OperationResult<LightNote.Domain.Models.NotebookAggregate.Notebook?>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
                }
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Notebook?>.CreateSuccess(notebook);
            }
            catch (Exception ex) {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Notebook?>.CreateFailure(new List<Exception>() { ex});
            }
        }
    }
}