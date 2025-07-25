namespace MyBudgetApp.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required int CategoryId { get; set; }
        public required int MonthNumber { get; set; }
        public required int Year { get; set; }
        public required decimal LimitAmount { get; set; }

        public Category Category { get; set; } = null!;
    }
}