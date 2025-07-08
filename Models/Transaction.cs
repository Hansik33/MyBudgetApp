using System;

namespace MyBudgetApp.Models
{
    public class Transaction
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required int CategoryId { get; set; }
        public required decimal Amount { get; set; }
        public required string Type { get; set; }
        public required string Description { get; set; }
        public required string PaymentMethod { get; set; }
        public required DateTime Date { get; set; }

        public required string CategoryName { get; set; }
    }
}