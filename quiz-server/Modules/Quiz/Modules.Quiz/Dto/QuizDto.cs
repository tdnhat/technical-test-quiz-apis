namespace Modules.Quiz.Dto
{
    public class QuizDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int PassingScore { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
}