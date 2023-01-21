using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightNote.Application.Helpers;
using MediatR;

namespace LightNote.Application.BusinessLogic.SlipNote.Commands
{
    public class DeleteSlipNote : IRequest<OperationResult<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserProfileId { get; set; }
    }
}