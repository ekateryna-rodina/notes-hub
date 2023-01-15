using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.SlipNote.Queries;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.QueryHandlers
{
    public class GetSlipNotesByNotebookIdHandler : IRequestHandler<GetSlipNotesByNotebookId, OperationResult<IEnumerable<Domain.Models.NotebookAggregate.Entities.SlipNote>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSlipNotesByNotebookIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<Domain.Models.NotebookAggregate.Entities.SlipNote>>> Handle(GetSlipNotesByNotebookId request, CancellationToken cancellationToken)
        {
            var slipNotes = await _unitOfWork.SlipNoteRepository.Get(s => s.NotebookId.Value == request.NotebookId && s.UserProfileId.Value == request.UserProfileId);
            return OperationResult<IEnumerable<Domain.Models.NotebookAggregate.Entities.SlipNote>>.CreateSuccess(slipNotes);
        }
    }
}