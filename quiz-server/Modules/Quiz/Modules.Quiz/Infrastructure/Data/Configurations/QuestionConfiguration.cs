using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Quiz.Infrastructure.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Domain.Question>
    {
        public void Configure(EntityTypeBuilder<Domain.Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Text).IsRequired().HasMaxLength(500);
            builder.Property(q => q.QuestionType).IsRequired();
            builder.Property(q => q.ImageUrl).IsRequired(false);
            builder.Property(q => q.Order).IsRequired();
            builder.HasMany(q => q.Answers)
                   .WithOne(a => a.Question)
                   .HasForeignKey(a => a.QuestionId);

            builder.HasMany(q => q.Answers)
                   .WithOne(a => a.Question)
                   .HasForeignKey(a => a.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(q => q.Quiz)
                   .WithMany(q => q.Questions)
                   .HasForeignKey(q => q.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(q => new { q.QuizId, q.Order })
                .IsUnique();
        }
    }
}
