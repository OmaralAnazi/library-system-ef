namespace LibrarySystem.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapControllers();
        return app;
    }

    public static WebApplication UseSwaggerIfDev(this WebApplication app)
    {
        if (!app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
}