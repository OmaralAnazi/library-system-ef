using LibrarySystem.Middlewares;

namespace LibrarySystem.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ApiExceptionMiddleware>();
}