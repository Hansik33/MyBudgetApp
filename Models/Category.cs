﻿namespace MyBudgetApp.Models
{
    public class Category
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int UserId { get; set; }
    }
}