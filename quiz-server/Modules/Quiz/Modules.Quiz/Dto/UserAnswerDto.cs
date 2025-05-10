namespace Modules.Quiz.Dto
{
    public class UserAnswerDto
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? SelectedAnswerId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsCorrect { get; set; }
        public string? Feedback { get; set; }
    }
}