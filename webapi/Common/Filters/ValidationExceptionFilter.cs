using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace webapi.Common.Filters
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is ValidationException validationException)
            {
                var errors = validationException.Errors
                    .Select(error => new
                    {
                        property = error.PropertyName,
                        error = error.ErrorMessage
                    }).ToList();

                context.Result = new BadRequestObjectResult(new
                {
                    message = "Errores de validación",
                    errors
                });

                context.ExceptionHandled = true;
            }
        }
    }
}
