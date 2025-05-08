namespace Modules.Quiz.Dto
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}