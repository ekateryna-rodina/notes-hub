using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.PermanentNote.Queries;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.PermanentNote.QueryHandlers
{
    public class GetPermanentNotesByNotebookIdHandler : IRequestHandler<GetPermanentNotesByNotebookId, OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermanentNotesByNotebookIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<LightNote.Domain.Models.NotebookAggregate.Entities.PermanentNote>>> Handle(GetPermanentNotesByNotebookId request, CancellationToken cancellationToken)
        {
            var permanentNotes = await _unitOfWork.PermanentNoteRepository.Get(s => s.NotebookId.Value == request.NotebookId && s.UserProfileId.Value == request.UserProfileId);
            return OperationResult<IEnumerable<Domain.Models.NotebookAggregate.Entities.PermanentNote>>.CreateSuccess(permanentNotes);
        }
    }
}

