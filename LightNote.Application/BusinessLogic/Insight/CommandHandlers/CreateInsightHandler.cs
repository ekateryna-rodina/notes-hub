using LightNote.Application.BusinessLogic.Insight.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.CommandHandlers
{
    public class CreateInsightHandler : IRequestHandler<CreateInsight, OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateInsightHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>> Handle(CreateInsight request, CancellationToken cancellationToken)
        {
            var notebook = await _unitOfWork.NotebookRepository.GetByID(request.NotebookId);
            if (notebook == null)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>.CreateFailure(new[] { new ResourceNotFoundException(nameof(LightNote.Domain.Models.NotebookAggregate.Notebook)) });
            }
            if (notebook.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            var permanentNotes = await _unitOfWork.PermanentNoteRepository.Get(r => request.PermanentNoteIds.Contains(r.Id.Value));
            if (!permanentNotes.Any())
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>.CreateFailure(new[] { new EntityIsRequiredException(nameof(LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote)) });
            }
            var insight = Domain.Models.NotebookAggregate.Entities.Insight
                    .Create(request.Title, request.Content, request.UserProfileId, request.NotebookId, permanentNotes);
            notebook.AddInsight(insight);
            try
            {
                _unitOfWork.InsightRepository.Insert(insight);
                _unitOfWork.NotebookRepository.Update(notebook);
                await _unitOfWork.SaveAsync();
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>.CreateSuccess(insight);
            }
            catch (Exception ex)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.Insight>.CreateFailure(new[] { ex });
            }
        }
    }
}