using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.BusinessLogic.Insight.Commands;
using LightNote.Application.Exceptions;
using LightNote.Application.Helpers;
using LightNote.Dal.Contracts;
using LightNote.Domain.Models.NotebookAggregate.ValueObjects;
using MediatR;

namespace LightNote.Application.BusinessLogic.Insight.CommandHandlers
{
    public class UpdateInsightContentHandler : IRequestHandler<UpdateInsight, OperationResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInsightContentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(UpdateInsight request, CancellationToken cancellationToken)
        {
            var insight = await _unitOfWork.InsightRepository.GetById(InsightId.Create(request.Id));
            if (insight == null)
            {
                return OperationResult<bool>.CreateFailure(new[] { new ResourceNotFoundException(nameof(Insight)) });
            }
            if (insight.UserProfileId.Value != request.UserProfileId)
            {
                return OperationResult<bool>.CreateFailure(new[] { new AccessIsNotAuthorizedException() });
            }
            insight.UpdateTitle(request.Title);
            insight.UpdateContent(request.Content);
            try
            {
                _unitOfWork.InsightRepository.Update(insight);
                await _unitOfWork.SaveAsync();
                return OperationResult<bool>.CreateSuccess(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(new[] { ex });
            }
        }
    }
}