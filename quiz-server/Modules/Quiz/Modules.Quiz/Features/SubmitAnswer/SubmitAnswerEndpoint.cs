using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Quiz.Domain;
using Modules.Quiz.Dto;
using Modules.Quiz.Infrastructure.Mappers;
using Modules.Quiz.Infrastructure.Repositories;

namespace Modules.Quiz.Features.SubmitAnswer
{
    public class SubmitAnswerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/quizzes/{id}/attempts/{attemptId}/answers", async (
                [FromRoute] Guid id,
                [FromRoute] Guid attemptId,
                [FromBody] SubmitAnswerRequestDto request,
                [FromServices] IQuizAttemptRepository quizAttemptRepo
            ) =>
            {
                // Get attempt
                var attempt = await quizAttemptRepo.GetByIdWithAnswersAsync(attemptId);
                if (attempt == null)
                {
                    return Results.NotFound("Attempt not found");
                }

                // Get question
                var question = attempt.Quiz.Questions.FirstOrDefault(q => q.Id == request.QuestionId);
                if (question == null)
                {
                    return Results.NotFound("Question not found");
                }

                // Validate selected answer
                var selectedAnswer = question.Answers.FirstOrDefault(a => a.Id == request.SelectedAnswerId);
                if (selectedAnswer == null)
                {
                    return Results.NotFound("Selected answer not found");
                }

                // Check if answer is correct
                var isCorrect = selectedAnswer.IsCorrect;

                // Update attempt
                var userAnswer = new UserAnswer
                {
                    Id = Guid.NewGuid(),
                    AttemptId = attempt.Id,
                    QuestionId = question.Id,
                    AnswerId = selectedAnswer.Id,
                    IsCorrect = isCorrect,
                    SubmittedAt = DateTime.UtcNow
                };

                await quizAttemptRepo.AddUserAnswerAsync(userAnswer);

                var userAnswerDto = userAnswer.ToDto();

                return Results.Ok(userAnswerDto);
            })
            .WithTags("Quizzes")
            .WithName("SubmitAnswer")
            .Produces<UserAnswerDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }

    }
}