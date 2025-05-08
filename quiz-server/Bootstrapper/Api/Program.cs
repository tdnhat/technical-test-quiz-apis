using Modules.Quiz;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddCoreServices();

builder.Services
    .AddQuizModule(builder.Configuration);

var app = builder.Build();

app.UseCoreMiddleware();

app.UseQuizModule();

app.Run();
