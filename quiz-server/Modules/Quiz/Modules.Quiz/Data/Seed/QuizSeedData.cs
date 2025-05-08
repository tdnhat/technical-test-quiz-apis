using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Modules.Quiz.Domain;
using Modules.Quiz.Infrastructure.Data;

namespace Modules.Quiz.Data.Seed
{
    public static class QuizSeedData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<QuizDbContext>>();

            try
            {
                context.Database.EnsureCreated();

                // Only seed if there are no quizzes
                if (!await context.Quizzes.AnyAsync())
                {
                    await SeedQuizzesAsync(context);
                    logger.LogInformation("Seed data created successfully.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        private static async Task SeedQuizzesAsync(QuizDbContext context)
        {
            // Create sample quizzes
            var cSharpQuiz = new Domain.Quiz
            {
                Id = Guid.NewGuid(),
                Title = "C# Basics",
                PassingScore = 70,
                CreatedAt = DateTime.UtcNow,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = Guid.NewGuid(),
                        QuestionType = "multiple-choice",
                        Order = 1,
                        Text = "What is the correct way to declare a variable in C#?",
                        Explanation = "In C#, there are several ways to declare variables. Type inference with 'var' was introduced in C# 3.0 and allows the compiler to determine the type based on the assigned value. This provides more concise code while maintaining type safety at compile time.",
                        Answers = new List<Answer>
                        {
                            new Answer { Id = Guid.NewGuid(), Text = "var x = 10;", IsCorrect = true },
                            new Answer { Id = Guid.NewGuid(), Text = "variable x = 10;", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "x = 10;", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "declare x = 10;", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        Id = Guid.NewGuid(),
                        QuestionType = "multiple-choice",
                        Order = 2,
                        Text = "Which of the following is a reference type in C#?",
                        Explanation = "C# has two main categories of types: value types and reference types. Value types (like int, enum, and struct) store their data directly in memory allocated on the stack. Reference types (like class, interface, and delegate) store a reference to their data, which is allocated on the heap. Understanding this distinction is crucial for memory management and performance considerations.",
                        Answers = new List<Answer>
                        {
                            new Answer { Id = Guid.NewGuid(), Text = "int", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "class", IsCorrect = true },
                            new Answer { Id = Guid.NewGuid(), Text = "enum", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "struct", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        Id = Guid.NewGuid(),
                        QuestionType = "multiple-choice",
                        Order = 3,
                        Text = "What does the 'async' keyword do in C#?",
                        Explanation = "The 'async' keyword in C# is part of the Task-based Asynchronous Pattern (TAP) introduced in C# 5.0. When applied to a method, it enables the use of the 'await' operator within that method. This doesn't automatically make code run on a separate thread - rather, it allows methods to be written that can suspend execution at 'await' points, releasing the thread to do other work and resuming later when the awaited operation completes.",
                        Answers = new List<Answer>
                        {
                            new Answer { Id = Guid.NewGuid(), Text = "Makes a method run on a separate thread", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "Marks a method for asynchronous execution", IsCorrect = true },
                            new Answer { Id = Guid.NewGuid(), Text = "Makes a method execute faster", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "Allows a method to be overridden", IsCorrect = false }
                        }
                    }
                }
            };

            var jsQuiz = new Domain.Quiz
            {
                Id = Guid.NewGuid(),
                Title = "JavaScript Fundamentals",
                PassingScore = 60,
                CreatedAt = DateTime.UtcNow,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Id = Guid.NewGuid(),
                        QuestionType = "multiple-choice",
                        Order = 1,
                        Text = "What is the correct way to declare a constant in JavaScript?",
                        Explanation = "JavaScript has three ways to declare variables: var, let, and const. Introduced in ES6 (2015), 'const' creates a block-scoped variable whose reference cannot be reassigned after initialization. While the reference is immutable, the contents of objects and arrays declared with const can still be modified. This is different from true constants in languages like C++ where the value itself is immutable.",
                        Answers = new List<Answer>
                        {
                            new Answer { Id = Guid.NewGuid(), Text = "const x = 10;", IsCorrect = true },
                            new Answer { Id = Guid.NewGuid(), Text = "let x = 10;", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "var x = 10;", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "constant x = 10;", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        Id = Guid.NewGuid(),
                        QuestionType = "multiple-choice",
                        Order = 2,
                        Text = "Which function is used to parse a string to an integer in JavaScript?",
                        Explanation = "JavaScript provides several methods for type conversion. The parseInt() function parses a string and returns an integer of the specified radix or base. It's important to note that parseInt() stops parsing when it encounters a non-numeric character, and it can take an optional second parameter to specify the radix (e.g., parseInt('FF', 16) returns 255). For floating-point conversion, parseFloat() would be used instead.",
                        Answers = new List<Answer>
                        {
                            new Answer { Id = Guid.NewGuid(), Text = "parseInteger()", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "Integer.parse()", IsCorrect = false },
                            new Answer { Id = Guid.NewGuid(), Text = "parseInt()", IsCorrect = true },
                            new Answer { Id = Guid.NewGuid(), Text = "int.parse()", IsCorrect = false }
                        }
                    }
                }
            };

            // Add quizzes to context
            await context.Quizzes.AddRangeAsync(cSharpQuiz, jsQuiz);
            await context.SaveChangesAsync();
        }
    }
}