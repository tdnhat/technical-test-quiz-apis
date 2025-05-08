using Carter;
using Modules.Quiz;
using Modules.Quiz.Data.Seed;
using Shared;

var builder = WebApplication.CreateBuilder(args);

var quizAssembly = typeof(QuizModuleExtensions).Assembly;

builder.Services.AddCarterWithAssemblies(quizAssembly);

builder.AddCoreServices();

builder.Services
    .AddQuizModule(builder.Configuration);

var app = builder.Build();

// Seed the database
await QuizSeedData.SeedAsync(app.Services);

app.MapCarter();

app.UseCoreMiddleware();

app.UseQuizModule();

app.Run();
