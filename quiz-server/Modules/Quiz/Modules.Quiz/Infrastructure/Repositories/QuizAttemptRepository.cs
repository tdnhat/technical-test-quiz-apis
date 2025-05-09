using Modules.Quiz.Domain;
using Modules.Quiz.Dto;
using Modules.Quiz.Infrastructure.Data;

namespace Modules.Quiz.Infrastructure.Repositories
{
    public class QuizAttemptRepository : IQuizAttemptRepository
    {
        private readonly QuizDbContext _context;

        public QuizAttemptRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<QuizAttempt?> AddAsync(QuizAttempt attempt)
        {
            await _context.QuizAttempts.AddAsync(attempt);
            await _context.SaveChangesAsync();
            return attempt;
        }

    }
}