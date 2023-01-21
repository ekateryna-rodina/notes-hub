using LightNote.Application.BusinessLogic.PermanentNote.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.CommandHandlers
{
    public class CreatePermanentNoteHandler : IRequestHandler<CreatePermanentNote, OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>>
    {
        private IUnitOfWork _unitOfWork;

        public CreatePermanentNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>> Handle(CreatePermanentNote request, CancellationToken cancellationToken)
        {
            var notebook = await _unitOfWork.NotebookRepository.GetById(NotebookId.Create(request.NotebookId));
            if (notebook == null)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>
                    .CreateFailure(new[] { new ResourceNotFoundException(nameof(LightNote.Domain.Models.NotebookAggregate.Notebook)) });
            }
            if (notebook.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>
                    .CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            var slipNotes = await _unitOfWork.SlipNoteRepository.Get(r => request.SlipNoteIds.Contains(r.Id.Value));
            if (!slipNotes.Any())
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>.CreateFailure(new[] { new EntityIsRequiredException(nameof(LightNote.Domain.Models.NotebookAggregate.Entities.SlipNote)) });
            }
            var permanentNote = Domain.Models.NotebookAggregate.Entities.PermanentNote
                    .Create(request.Title, request.Content, request.UserProfileId, request.NotebookId, slipNotes);
            notebook.AddPermanentNote(permanentNote);
            try
            {
                _unitOfWork.PermanentNoteRepository.Insert(permanentNote);
                _unitOfWork.NotebookRepository.Update(notebook);
                await _unitOfWork.SaveAsync();
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>.CreateSuccess(permanentNote);
            }
            catch (Exception ex)
            {
                return OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>.CreateFailure(new[] { ex });
            }
        }
    }
}