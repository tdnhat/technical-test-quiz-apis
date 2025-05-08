using Modules.Quiz.Dto;

namespace Modules.Quiz.Infrastructure.Mappers
{
    public static class QuizMapper
    {
        public static QuizDto ToDto(this Domain.Quiz quiz)
        {
            return new QuizDto
            {
                Id = quiz.Id,
                Title = quiz.Title,
                PassingScore = quiz.PassingScore,
                Questions = quiz.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Order = q.Order,
                    QuestionType = q.QuestionType,
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };
        }
    }
}