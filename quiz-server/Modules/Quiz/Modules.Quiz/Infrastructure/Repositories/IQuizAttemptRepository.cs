using Modules.Quiz.Domain;

namespace Modules.Quiz.Infrastructure.Repositories
{
    public interface IQuizAttemptRepository
    {
        Task<QuizAttempt?> AddAsync(QuizAttempt attempt);
    }
}