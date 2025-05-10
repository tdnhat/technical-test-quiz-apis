using Modules.Quiz.Domain;
using Modules.Quiz.Dto;
using System.Linq;

namespace Modules.Quiz.Infrastructure.Mappers
{
    public static class QuizAttemptMapper
    {
        public static QuizAttemptDto ToDto(this QuizAttempt attempt)
        {
            return new QuizAttemptDto
            {
                AttemptId = attempt.Id,
                QuizId = attempt.QuizId,
                UserId = attempt.UserId,
                StartedAt = attempt.StartedAt,
            };
        }

        public static QuizCompletionDto ToCompletionDto(this QuizAttempt attempt)
        {
            return new QuizCompletionDto
            {
                QuizId = attempt.QuizId,
                UserId = attempt.UserId,
                Status = attempt.Status,
                Score = attempt.Score ?? 0,
                IsPassed = attempt.IsPassed ?? false,
                TimeSpent = attempt.TimeSpent ?? TimeSpan.Zero
            };
        }

        public static QuizResultDto ToResultDto(this QuizAttempt attempt, Domain.Quiz quiz)
        {
            var totalQuestions = quiz.Questions.Count;
            var correctAnswers = attempt.UserAnswers.Count(x => x.IsCorrect == true);
            var incorrectAnswers = totalQuestions - correctAnswers;

            var result = new QuizResultDto
            {
                QuizId = attempt.QuizId,
                UserId = attempt.UserId,
                Status = attempt.Status,
                Score = attempt.Score ?? 0,
                IsPassed = attempt.IsPassed ?? false,
                TimeSpent = attempt.TimeSpent ?? TimeSpan.Zero,
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers,
                IncorrectAnswers = incorrectAnswers,
                AnswerReviews = CreateAnswerReviews(attempt, quiz)
            };

            return result;
        }

        private static List<QuizAnswerReviewDto> CreateAnswerReviews(QuizAttempt attempt, Domain.Quiz quiz)
        {
            var answerReviews = new List<QuizAnswerReviewDto>();
            
            foreach (var question in quiz.Questions.OrderBy(x => x.Order))
            {
                // Find user's answer for this question
                var userAnswer = attempt.UserAnswers.FirstOrDefault(ua => ua.QuestionId == question.Id);

                // Find correct answer for this question
                var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);
                if (correctAnswer == null)
                {
                    continue;
                }

                // Get the answer text that the user selected (if any)
                var selectedAnswer = userAnswer?.AnswerId != null 
                    ? question.Answers.FirstOrDefault(a => a.Id == userAnswer.AnswerId) 
                    : null;

                answerReviews.Add(new QuizAnswerReviewDto
                {
                    QuestionId = question.Id,
                    QuestionText = question.Text,
                    CorrectAnswerText = correctAnswer.Text,
                    UserSelectedAnswerText = selectedAnswer?.Text ?? "No answer selected",
                    IsCorrect = userAnswer?.IsCorrect ?? false
                });
            }

            return answerReviews;
        }
    }
}