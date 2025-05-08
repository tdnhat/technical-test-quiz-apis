using Shared.Domain;

namespace Modules.Quiz.Domain
{
    public class Question : Entity
    {
        public Guid QuizId { get; set; }
        public string QuestionType { get; set; } = "multiple-choice";
        public string Text { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? Explanation { get; set; }
        public List<Answer> Answers { get; set; } = new();
        public Quiz Quiz { get; set; } = new();
    }
}
