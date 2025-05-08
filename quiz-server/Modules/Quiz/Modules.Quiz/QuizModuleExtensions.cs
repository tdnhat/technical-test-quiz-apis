using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Quiz.Infrastructure.Data;

namespace Modules.Quiz
{
    public static class QuizModuleExtensions
    {
        public static IServiceProvider AddQuizModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add quiz module services here if needed
            services.AddDbContext<QuizDbContext>(options =>
                           options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services.BuildServiceProvider();
        }

        public static WebApplication UseQuizModule(this WebApplication app)
        {
            // Configure quiz module middleware here if needed

            return app;
        }
    }
}
