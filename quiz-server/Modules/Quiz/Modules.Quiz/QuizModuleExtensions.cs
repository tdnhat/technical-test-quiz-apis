using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Quiz.Infrastructure;
using Modules.Quiz.Infrastructure.Data;
using Modules.Quiz.Infrastructure.Repositories;

namespace Modules.Quiz
{
    public static class QuizModuleExtensions
    {
        public static IServiceProvider AddQuizModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add quiz module services here if needed
            services.AddDbContext<QuizDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Modules.Quiz")));

            // Register repositories
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();

            return services.BuildServiceProvider();
        }

        public static WebApplication UseQuizModule(this WebApplication app)
        {
            // Apply migrations
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
            
            try
            {
                // Check if tables already exist
                if (!context.Quizzes.Any())
                {
                    context.Database.Migrate();
                }
            }
            catch
            {
                // If there's an error, try to ensure the database is created
                context.Database.EnsureCreated();
            }
            
            return app;
        }
    }
}
