using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Quiz.Infrastructure.Repositories;
using Modules.Quiz.Dto;
using Modules.Quiz.Infrastructure;
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

                var totalQuestions = quiz.Questions.Count;
                var correctAnswers = attempt.UserAnswers.Count(x => x.IsCorrect == true);
                var incorrectAnswers = totalQuestions - correctAnswers;

                // Create answer reviews for each question
                var answerReviews = new List<QuizAnswerReviewDto>();
                foreach (var question in quiz.Questions.OrderBy(x => x.Order))
                {
                    // Find user's answer for this question
                    var userAnswer = attempt.UserAnswers.FirstOrDefault(ua => ua.QuestionId == question.Id);

                    // Find correct answer for this question
                    var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);
                    if (correctAnswer == null)
                    {
                        continue;
                    }

                    // Get the answer text that the user selected (if any)
                    var selectedAnswer = userAnswer?.AnswerId != null
                        ? question.Answers.FirstOrDefault(a => a.Id == userAnswer.AnswerId)
                        : null;

                    answerReviews.Add(new QuizAnswerReviewDto
                    {
                        QuestionId = question.Id,
                        QuestionText = question.Text,
                        CorrectAnswerText = correctAnswer.Text,
                        UserSelectedAnswerText = selectedAnswer?.Text ?? "No answer selected",
                        IsCorrect = userAnswer?.IsCorrect ?? false,
                    });
                }

                var result = new QuizResultDto
                {
                    QuizId = attempt.QuizId,
                    UserId = attempt.UserId,
                    Status = attempt.Status,
                    Score = attempt.Score ?? 0,
                    IsPassed = attempt.IsPassed ?? false,
                    TimeSpent = attempt.TimeSpent ?? TimeSpan.Zero,
                    TotalQuestions = totalQuestions,
                    CorrectAnswers = correctAnswers,
                    IncorrectAnswers = incorrectAnswers,
                    AnswerReviews = answerReviews
                };

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