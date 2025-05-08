using Shared.Domain;

namespace Modules.Quiz.Domain
{
    public class UserAnswer : Entity
    {
        public Guid AttemptId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? AnswerId { get; set; } 
        public DateTime SubmittedAt { get; set; }
        public bool? IsCorrect { get; set; }
        public QuizAttempt QuizAttempt { get; set; } = new();
        public Question Question { get; set; } = new();
        public Answer Answer { get; set; } = new();
    }
}
