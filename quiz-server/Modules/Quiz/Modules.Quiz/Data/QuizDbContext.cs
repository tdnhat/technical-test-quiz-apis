using Microsoft.EntityFrameworkCore;
using Modules.Quiz.Domain;

namespace Modules.Quiz.Infrastructure.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
        }
        public DbSet<Domain.Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("quiz");

            // Configure the entity mappings
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
            // Add your entity configurations here
        }
    }
}
