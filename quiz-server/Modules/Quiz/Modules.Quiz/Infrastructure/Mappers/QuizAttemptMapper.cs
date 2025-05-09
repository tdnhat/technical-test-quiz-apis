using Modules.Quiz.Domain;
using Modules.Quiz.Dto;

namespace Modules.Quiz.Infrastructure.Mappers
{
    public static class QuizAttemptMapper
    {
        public static QuizAttemptDto ToDto(this QuizAttempt attempt)
        {
            return new QuizAttemptDto
            {
                Id = attempt.Id,
                QuizId = attempt.QuizId,
                UserId = attempt.UserId,
                StartedAt = attempt.StartedAt
            };
        }
    }
}