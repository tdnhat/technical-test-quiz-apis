using Modules.Quiz.Domain;
using Modules.Quiz.Dto;

namespace Modules.Quiz.Infrastructure.Mappers
{
    public static class UserAnswerMapper
    {
        public static UserAnswerDto ToDto(this UserAnswer userAnswer)
        {
            var feedback = userAnswer.IsCorrect == true ? "Correct!" : "Incorrect!";
            return new UserAnswerDto
            {
                Id = userAnswer.Id,
                QuestionId = userAnswer.QuestionId,
                SelectedAnswerId = userAnswer.AnswerId,
                IsCorrect = userAnswer.IsCorrect ?? false,
                SubmittedAt = userAnswer.SubmittedAt,
                Feedback = feedback
            };
        }
    }
}