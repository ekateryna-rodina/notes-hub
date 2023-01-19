using System;
using LightNote.Api.Contracts.Common;
using LightNote.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LightNote.Api.Extensions
{
	public static class ControllerExtensions
	{
        public static BadRequestObjectResult BadRequestWithErrors(this ControllerBase controller, IEnumerable<Exception> errors)
        {
            var apiError = new ErrorResponse(400, "Bad request");
            apiError.Errors.AddRange(errors.Select(e => e.Message));
            return controller.BadRequest(apiError);
        }
    }
}


