namespace Modules.Quiz.Dto
{
    public class QuizResultDto
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public double Score { get; set; }
        public bool IsPassed { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }
        public List<QuizAnswerReviewDto> AnswerReviews { get; set; } = new();
    }

    public class QuizAnswerReviewDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswerText { get; set; }
        public string UserSelectedAnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}