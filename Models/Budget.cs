namespace MyBudgetApp.Models
{
    public class Budget
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required int CategoryId { get; set; }
        public required int MonthNumber { get; set; }
        public required int Month { get; set; }
        public required decimal LimitAmount { get; set; }

        public required string CategoryName { get; set; }
    }
}