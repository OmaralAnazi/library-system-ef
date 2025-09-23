using System.Net;
using System.Text.Json;
using LibrarySystem.Exceptions;

namespace LibrarySystem.Middlewares;

public sealed class ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");

            context.Response.ContentType = "application/json";

            if (ex is ApiException api)
            {
                context.Response.StatusCode = (int)api.StatusCode;

                var payload = new
                {
                    code = api.ErrorCode,
                    englishDescription = api.EnglishDescription,
                    arabicDescription  = api.ArabicDescription,
                    traceId = context.TraceIdentifier
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
                return;
            }

            // fallback 500
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var fallback = new
            {
                code = "E9999",
                englishDescription = "An unexpected error occurred.",
                arabicDescription  = "حدث خطأ غير متوقع.",
                traceId = context.TraceIdentifier
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(fallback));
        }
    }
}