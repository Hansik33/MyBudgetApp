using System;

namespace MyBudgetApp.Models
{
    public class Saving
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required int GoalId { get; set; }
        public required decimal Amount { get; set; }
        public required DateTime Date { get; set; }
    }
}
