using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Quiz.Domain;
using Modules.Quiz.Dto;
using Modules.Quiz.Infrastructure.Mappers;
using Modules.Quiz.Infrastructure.Repositories;

namespace Modules.Quiz.Features.StartQuiz
{
    public class StartQuizEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/quizzes/{id}/attempts", async (
                [FromRoute] Guid id,
                [FromServices] IQuizAttemptRepository quizAttemptRepo,
                [FromServices] IHttpContextAccessor httpContextAccessor
            ) =>
            {
                var context = httpContextAccessor.HttpContext;
                if (context == null)
                {
                    return Results.BadRequest("HttpContext is null");
                }

                // Get or create user id
                var userId = context.Session.GetString("UserId") ?? Guid.NewGuid().ToString();
                context.Session.SetString("UserId", userId);

                // Create attempt
                var attempt = new QuizAttempt
                {
                    QuizId = id,
                    UserId = Guid.Parse(userId),
                    StartedAt = DateTime.UtcNow
                };
                var savedAttempt = await quizAttemptRepo.AddAsync(attempt);

                if (savedAttempt == null)
                {
                    return Results.BadRequest("Failed to create attempt");
                }

                var quizAttemptDto = savedAttempt.ToDto();

                // Return attempt
                return Results.Created($"/api/quizzes/{id}/attempts/{savedAttempt.Id}", quizAttemptDto);
            })
            .WithTags("Quizzes")
            .WithName("StartQuiz")
            .Produces<QuizAttemptDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }

    }
}