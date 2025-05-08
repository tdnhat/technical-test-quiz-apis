namespace Modules.Quiz.Infrastructure
{
    public interface IQuizRepository
    {
        Task<Domain.Quiz?> GetByIdWithQuestionsAndAnswersAsync(Guid id);
    }
}