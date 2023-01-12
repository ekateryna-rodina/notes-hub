using System;
using LightNote.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LightNote.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var error = new ErrorResponse(400, "Bad request");
                var errors = context.ModelState.AsEnumerable();
                foreach (var e in errors)
                {
                    if (e.Value == null) continue;
                    foreach (var inner in e.Value.Errors)
                    {
                        error.Errors.Add(inner.ErrorMessage);
                    }
                }
                context.Result = new BadRequestObjectResult(error);
            }
        }
    }
}

