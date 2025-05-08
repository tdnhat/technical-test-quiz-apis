using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain;

namespace Modules.Quiz.Infrastructure.Data.Configurations
{
    public class UserAnswerConfiguration : IEntityTypeConfiguration<Domain.UserAnswer>
    {
        public void Configure(EntityTypeBuilder<Domain.UserAnswer> builder)
        {
            builder.ToTable("UserAnswers");
            builder.HasKey(ua => ua.Id);
            builder.Property(ua => ua.AttemptId).IsRequired();
            builder.Property(ua => ua.QuestionId).IsRequired();
            builder.Property(ua => ua.AnswerId).IsRequired(false);
            builder.Property(ua => ua.IsCorrect).IsRequired(false);
            builder.Property(ua => ua.SubmittedAt).IsRequired();

            builder.HasOne(ua => ua.QuizAttempt)
                .WithMany(qa => qa.UserAnswers)
                .HasForeignKey(ua => ua.AttemptId);

            builder.HasOne(ua => ua.Question)
                .WithMany()
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ua => ua.Answer)
                .WithMany()
                .HasForeignKey(ua => ua.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
