using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Quiz.Infrastructure.Data.Configurations
{
    public class QuizAttemptConfiguration : IEntityTypeConfiguration<Domain.QuizAttempt>
    {
        public void Configure(EntityTypeBuilder<Domain.QuizAttempt> builder)
        {
            builder.ToTable("QuizAttempts");
            builder.HasKey(qa => qa.Id);
            builder.Property(qa => qa.UserId).IsRequired();
            builder.Property(qa => qa.QuizId).IsRequired();
            builder.Property(qa => qa.StartedAt).IsRequired();
            builder.Property(qa => qa.CompletedAt).IsRequired(false);
            builder.Property(qa => qa.Score).IsRequired(false);
            builder.Property(qa => qa.Status).IsRequired().HasMaxLength(20);

            builder.HasOne(qa => qa.Quiz)
                .WithMany()
                .HasForeignKey(qa => qa.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(qa => qa.UserAnswers)
                .WithOne(ua => ua.QuizAttempt)
                .HasForeignKey(ua => ua.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
