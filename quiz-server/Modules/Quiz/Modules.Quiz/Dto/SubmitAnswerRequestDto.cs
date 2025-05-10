namespace Modules.Quiz.Dto
{
    public class SubmitAnswerRequestDto
    {
        public Guid QuestionId { get; set; }
        public Guid SelectedAnswerId { get; set; }
    }
}