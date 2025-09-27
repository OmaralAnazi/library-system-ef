using LibrarySystem.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddSwaggerIfDev(builder.Environment)
    .AddPersistence(builder.Configuration)
    .AddFluentValidators()
    .AddApplication()
    .AddMappings();

var app = builder.Build();

app.UseApiExceptionHandling();

app.UseSwaggerIfDev()
    .UseApi();

app.InitializeDatabase();

app.Run();