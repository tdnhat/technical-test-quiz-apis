namespace Modules.Quiz.Infrastructure
{
    public interface IQuizRepository
    {
        Task<Domain.Quiz?> GetByIdAsync(Guid id);
        Task<Domain.Quiz?> GetByIdWithQuestionsAndAnswersAsync(Guid id);
    }
}