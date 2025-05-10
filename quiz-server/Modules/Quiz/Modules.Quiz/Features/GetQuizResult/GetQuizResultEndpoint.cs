using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Quiz.Infrastructure.Repositories;
using Modules.Quiz.Dto;
using Modules.Quiz.Infrastructure;
using Modules.Quiz.Infrastructure.Mappers;
using System.Linq;

namespace Modules.Quiz.Features.GetQuizResult
{
    public class GetQuizResultEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/attempts/{attemptId}/results", async (
                Guid attemptId,
                [FromServices] IQuizAttemptRepository quizAttemptRepo,
                [FromServices] IQuizRepository quizRepo
            ) =>
            {
                // Get the attempt
                var attempt = await quizAttemptRepo.GetByIdWithAnswersAsync(attemptId);
                if (attempt == null)
                {
                    return Results.NotFound("Quiz attempt not found");
                }

                // Check if the quiz has been completed
                if (attempt.Status != "completed")
                {
                    return Results.BadRequest("Quiz has not been completed yet");
                }

                // Get the quiz
                var quiz = await quizRepo.GetByIdWithQuestionsAndAnswersAsync(attempt.QuizId);
                if (quiz == null)
                {
                    return Results.NotFound("Quiz not found");
                }

                // Map to result DTO
                var result = attempt.ToResultDto(quiz);
                
                return Results.Ok(result);
            })
            .WithTags("Quizzes")
            .WithName("GetQuizResult")
            .Produces<QuizResultDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}