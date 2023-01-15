using LightNote.Application.BusinessLogic.SlipNote.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.CommandHandlers
{
    public class CreateSlipNoteHandler : IRequestHandler<CreateSlipNote, OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.SlipNote>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSlipNoteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote>> Handle(CreateSlipNote request, CancellationToken cancellationToken)
        {
            var notebook = await _unitOfWork.NotebookRepository.GetByID(request.NotebookId);
            if (notebook == null)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote>.CreateFailure(new[] { new ResourceNotFoundException(nameof(Notebook)) });
            }
            if (notebook.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            var references = await _unitOfWork.ReferenceRepository.Get(r => request.ReferenceIds.Contains(r.Id.Value));
            if (!references.Any())
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote>.CreateFailure(new[] { new EntityIsRequiredException(nameof(Reference)) });
            }
            var slipNote = Domain.Models.NotebookAggregate.Entities.SlipNote
                    .Create(request.Content, request.UserProfileId, request.NotebookId, references);
            notebook.AddSlipNote(slipNote);
            try
            {
                _unitOfWork.SlipNoteRepository.Insert(slipNote);
                _unitOfWork.NotebookRepository.Update(notebook);
                await _unitOfWork.SaveAsync();
                return OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote>.CreateSuccess(slipNote);
            }
            catch (Exception ex)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.SlipNote>.CreateFailure(new[] { ex });
            }
        }
    }
}