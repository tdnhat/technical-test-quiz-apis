using Modules.Quiz.Domain;

namespace Modules.Quiz.Infrastructure.Repositories
{
    public interface IQuizAttemptRepository
    {
        Task<QuizAttempt?> GetByIdAsync(Guid id);
        Task<QuizAttempt?> GetByIdWithAnswersAsync(Guid id);
        Task<QuizAttempt?> AddAsync(QuizAttempt attempt);
        Task<UserAnswer?> AddUserAnswerAsync(UserAnswer userAnswer);
        Task<QuizAttempt?> UpdateAsync(QuizAttempt attempt);
    }
}