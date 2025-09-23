using LibrarySystem.Data;
using LibrarySystem.Repositories;
using LibrarySystem.Services;
using Microsoft.EntityFrameworkCore;

// TODO: clean up this file (refactor)

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.DefaultIgnoreCondition =
        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddEndpointsApiExplorer();
if (!builder.Environment.IsProduction())
{
    builder.Services.AddSwaggerGen();
}

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// DB UoW and Repositories
builder.Services.AddScoped<IDbUnitOfWork, DbUnitOfWork>();
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IMembersRepository, MembersRepository>();
builder.Services.AddScoped<ILoansRepository, LoansRepository>();

// Services
builder.Services.AddScoped<IBooksService, BooksService>();

// Mappers
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();