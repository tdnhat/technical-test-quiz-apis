using Carter;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Modules.Quiz.Infrastructure.Repositories;
using Modules.Quiz.Infrastructure.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Modules.Quiz.Infrastructure;
using Modules.Quiz.Dto;
namespace Modules.Quiz.Features.CompleteQuiz
{
    public class CompleteQuizEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/attempts/{attemptId}/complete", async (
                [FromRoute] Guid attemptId,
                [FromServices] IQuizAttemptRepository quizAttemptRepo,
                [FromServices] IQuizRepository quizRepo
            ) =>
            {
                var attempt = await quizAttemptRepo.GetByIdWithAnswersAsync(attemptId);
                if (attempt == null)
                {
                    return Results.NotFound("Quiz attempt not found");
                }

                var quiz = await quizRepo.GetByIdWithQuestionsAndAnswersAsync(attempt.QuizId);
                if (quiz == null)
                {
                    return Results.NotFound("No quiz found for this attempt");
                }

                if (attempt.CompletedAt != null)
                {
                    return Results.BadRequest("Quiz attempt already completed");
                }

                var totalQuestions = quiz.Questions.Count;
                var correctAnswers = attempt.UserAnswers.Count(x => x.IsCorrect == true);
                var score = totalQuestions > 0 ? correctAnswers * 100 / (double)totalQuestions : 0;
                var isPassed = score >= quiz.PassingScore;

                if (attempt.Status == "in-progress")
                {
                    attempt.CompletedAt = DateTime.UtcNow;
                    attempt.TimeSpent = attempt.CompletedAt.Value - attempt.StartedAt;
                    attempt.Score = score;
                    attempt.IsPassed = isPassed;
                    attempt.Status = "completed";

                    await quizAttemptRepo.UpdateAsync(attempt);
                }

                var completionResult = attempt.ToCompletionDto();
                
                return Results.Ok(completionResult);
            })
            .WithTags("Quizzes")
            .WithName("CompleteQuiz")
            .Produces<QuizCompletionDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}