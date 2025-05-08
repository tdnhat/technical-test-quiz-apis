using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Quiz.Infrastructure.Data.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Domain.Answer>
    {
        public void Configure(EntityTypeBuilder<Domain.Answer> builder)
        {
            builder.ToTable("Answers");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Text).IsRequired().HasMaxLength(500);
            builder.Property(a => a.IsCorrect).IsRequired();
        }
    }
}
