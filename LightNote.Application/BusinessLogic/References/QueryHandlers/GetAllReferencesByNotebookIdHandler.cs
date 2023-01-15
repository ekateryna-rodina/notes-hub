using System;
using LightNote.Application.BusinessLogic.References.Queries;
using LightNote.Application.Helpers;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Application.BusinessLogic.References.QueryHandlers
{
    public class GetAllReferencesByNotebookIdHandler : IRequestHandler<GetReferencesByNotebookId, OperationResult<IEnumerable<Reference>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllReferencesByNotebookIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<Reference>>> Handle(GetReferencesByNotebookId request, CancellationToken cancellationToken)
        {
            var references = await _unitOfWork
                .ReferenceRepository.Get(r => request.UserProfileId == r.UserProfileId.Value
                        && request.NotebookId == r.NotebookId.Value);
            return OperationResult<IEnumerable<Reference>>.CreateSuccess(references);
        }
    }
}

