namespace Modules.Quiz.Dto
{
    public class QuizCompletionDto
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public double Score { get; set; }
        public bool IsPassed { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }
} 