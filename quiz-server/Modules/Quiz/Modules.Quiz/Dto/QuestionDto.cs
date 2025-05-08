namespace Modules.Quiz.Dto
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string QuestionType { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public List<AnswerDto> Answers { get; set; } = new();
    }
}