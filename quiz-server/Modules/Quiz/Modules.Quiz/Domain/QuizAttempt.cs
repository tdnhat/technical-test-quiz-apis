﻿using Shared.Domain;

namespace Modules.Quiz.Domain
{
    public class QuizAttempt : Entity
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public double? Score { get; set; }
        public bool? IsPassed { get; set; }
        public TimeSpan? TimeSpent { get; set; }
        public string Status { get; set; } = "in-progress"; // in-progress, completed, abandoned
        public List<UserAnswer> UserAnswers { get; set; } = new();
        public Quiz Quiz { get; set; }
    }
}
