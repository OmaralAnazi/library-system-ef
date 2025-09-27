using FluentValidation;
using FluentValidation.Results;
using LibrarySystem.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibrarySystem.Middlewares;

public sealed class FluentValidationActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext ctx, ActionExecutionDelegate next)
    {
        var ct = ctx.HttpContext.RequestAborted;
        var failures = new List<ValidationFailure>();

        foreach (var arg in ctx.ActionArguments.Values)
        {
            if (arg is null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(arg.GetType());
            var validator = ctx.HttpContext.RequestServices.GetService(validatorType) as IValidator;

            if (validator is null) continue; // no validator registered for this type

            var result = await validator.ValidateAsync(new ValidationContext<object>(arg), ct);
            if (!result.IsValid) failures.AddRange(result.Errors);
        }

        if (failures.Count > 0)
            throw ExceptionFactory.BadRequestException(); // TODO: Could be enhanced to return what is the bad request about

        await next();
    }
}