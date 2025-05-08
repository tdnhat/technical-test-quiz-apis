using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Quiz.Infrastructure.Data.Configurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<Domain.Quiz>
    {
        public void Configure(EntityTypeBuilder<Domain.Quiz> builder)
        {
            builder.ToTable("Quizzes");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Title).IsRequired().HasMaxLength(200);
            builder.Property(q => q.CreatedAt).IsRequired();
            builder.Property(q => q.UpdatedAt).IsRequired(false);
            builder.HasMany(q => q.Questions)
                   .WithOne(q => q.Quiz)
                   .HasForeignKey(q => q.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
