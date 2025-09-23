using LibrarySystem.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddSwaggerIfDev(builder.Environment)
    .AddPersistence(builder.Configuration)
    .AddApplication()
    .AddMappings();

var app = builder.Build();

app.UseSwaggerIfDev()
    .UseApi();

app.Run();