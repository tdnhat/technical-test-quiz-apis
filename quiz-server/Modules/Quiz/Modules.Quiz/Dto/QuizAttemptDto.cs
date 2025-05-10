namespace Modules.Quiz.Dto
{
    public class QuizAttemptDto
    {
        public Guid AttemptId { get; set; }
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartedAt { get; set; }
    }
}