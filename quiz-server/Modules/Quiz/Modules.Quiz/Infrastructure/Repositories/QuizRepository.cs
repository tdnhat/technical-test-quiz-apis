
using Microsoft.EntityFrameworkCore;
using Modules.Quiz.Infrastructure.Data;

namespace Modules.Quiz.Infrastructure
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizDbContext _context;

        public QuizRepository(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Quiz?> GetByIdWithQuestionsAndAnswersAsync(Guid id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);
            return quiz;
        }
    }
}