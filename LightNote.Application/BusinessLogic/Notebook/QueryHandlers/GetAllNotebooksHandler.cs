using LightNote.Application.BusinessLogic.Notebook.Queries;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Notebook.QueryHandlers
{
    public class GetAllNotebooksHandler : IRequestHandler<GetAllNotebooks, OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Notebook>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllNotebooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Notebook>>> Handle(GetAllNotebooks request, CancellationToken cancellationToken)
        {
            try
            {
                var notebooks = await _unitOfWork.NotebookRepository.Get(n => n.UserProfileId.Value == request.UserProfileId);
                return OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Notebook>>.CreateSuccess(notebooks);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Notebook>>.CreateFailure(new[] { ex });
            }
        }
    }
}