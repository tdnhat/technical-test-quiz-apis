using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Modules.Quiz.Dto;
using Modules.Quiz.Infrastructure;
using Modules.Quiz.Infrastructure.Mappers;

namespace Modules.Quiz.Features.GetQuizQuestions
{
    public class GetQuizQuestionsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/quizzes/{quizId}/questions", async (
                [FromRoute] Guid quizId,
                [FromServices] IQuizRepository quizRepo
            ) =>
            {
                var quiz = await quizRepo.GetByIdWithQuestionsAndAnswersAsync(quizId);
                if (quiz is null)
                {
                    return Results.NotFound($"Quiz with quizId {quizId} not found");
                }

                var quizDto = quiz.ToDto();
                return Results.Ok(quizDto);
            })
            .WithTags("Quizzes")
            .WithName("GetQuizQuestions")
            .Produces<QuizDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}