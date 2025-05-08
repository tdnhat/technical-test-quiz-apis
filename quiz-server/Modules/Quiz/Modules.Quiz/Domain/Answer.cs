using Shared.Domain;

namespace Modules.Quiz.Domain
{
    public class Answer : Entity
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public Question Question { get; set; } = new();
    }
}
