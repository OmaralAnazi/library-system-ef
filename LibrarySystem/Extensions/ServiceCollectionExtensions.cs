using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

namespace LibrarySystem.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            o.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

        services.Configure<RouteOptions>(o => o.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        return services;
    }

    public static IServiceCollection AddSwaggerIfDev(this IServiceCollection services, IWebHostEnvironment env)
    {
        if (!env.IsProduction())
            services.AddSwaggerGen();
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<LibraryContext>(opt =>
            opt.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        // UoW + Repos
        services.AddScoped<IDbUnitOfWork, DbUnitOfWork>();
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IMembersRepository, MembersRepository>();
        services.AddScoped<ILoansRepository, LoansRepository>();
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IBooksService, BooksService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<ILoansService, LoansService>();
        return services;
    }

    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
        return services;
    }
}
