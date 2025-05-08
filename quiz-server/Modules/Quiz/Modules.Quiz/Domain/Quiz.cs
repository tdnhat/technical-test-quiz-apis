using Shared.Domain;

namespace Modules.Quiz.Domain
{
    public class Quiz : Entity
    {
        public string Title { get; set; } = string.Empty;
        public int PassingScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Question> Questions { get; set; } = new();
    }
}
