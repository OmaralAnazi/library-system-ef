using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;

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
    
    public static WebApplication InitializeDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<LibraryContext>();
        
        if (!context.Database.GetAppliedMigrations().Any())
            context.Database.Migrate();
        
        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();
        
        context.Database.EnsureCreated();

        return app;
    }
}