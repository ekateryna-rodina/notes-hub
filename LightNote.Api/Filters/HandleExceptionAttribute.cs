using System;
using LightNote.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LightNote.Api.Filters
{
	public class HandleExceptionAttribute : ExceptionFilterAttribute
	{
        public override void OnException(ExceptionContext context)
        {
            var apiError = new ErrorResponse
            {
                Code = 500,
                Phrase = "Internal Server Error",
                Timestamp = DateTime.Now
            };
            apiError.Errors.Add(context.Exception.Message);
            context.Result = new JsonResult(apiError) {
                StatusCode = 500
            };
        }
    }
}

