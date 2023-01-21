using LightNote.Application.BusinessLogic.PermanentNote.Queries;
using LightNote.Application.BusinessLogic.SlipNote.Queries;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.QueryHandlers
{
    public class GetPermanentNoteByIdHandler : IRequestHandler<GetPermanentNoteById, OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote?>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermanentNoteByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote?>> Handle(GetPermanentNoteById request, CancellationToken cancellationToken)
        {
            var permanentNote = await _unitOfWork.PermanentNoteRepository.GetById(PermanentNoteId.Create(request.Id));
            if (permanentNote == null)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.PermanentNote?>
                    .CreateFailure(new List<Exception>() { new ResourceNotFoundException(nameof(PermanentNote)) });
            }
            if (permanentNote!.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<Domain.Models.NotebookAggregate.Entities.PermanentNote?>
                    .CreateFailure(new List<Exception>() { new AccessIsNotAuthorizedException() });
            }
            return OperationResult<Domain.Models.NotebookAggregate.Entities.PermanentNote?>.CreateSuccess(permanentNote);
        }
    }
}