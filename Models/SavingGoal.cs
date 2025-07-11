using System;

namespace MyBudgetApp.Models
{
    public class SavingGoal
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public required decimal TargetAmount { get; set; }
        public required decimal SavedAmount { get; set; }
        public required DateTime Deadline { get; set; }
    }
}