using Microsoft.EntityFrameworkCore;
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

        public async Task<UserAnswer?> AddUserAnswerAsync(UserAnswer userAnswer)
        {
            await _context.UserAnswers.AddAsync(userAnswer);
            await _context.SaveChangesAsync();
            return userAnswer;
        }

        public async Task<QuizAttempt?> GetByIdAsync(Guid id)
        {
            return await _context.QuizAttempts.FindAsync(id);
        }


        public async Task<QuizAttempt?> GetByIdWithAnswersAsync(Guid id)
        {
            return await _context.QuizAttempts
                .Include(a => a.UserAnswers)
                .Include(a => a.Quiz)
                    .ThenInclude(q => q.Questions)
                        .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<QuizAttempt?> UpdateAsync(QuizAttempt attempt)
        {
            _context.QuizAttempts.Update(attempt);
            await _context.SaveChangesAsync();
            return attempt;
        }

    }
}