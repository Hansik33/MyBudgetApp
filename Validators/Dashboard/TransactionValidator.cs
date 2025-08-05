using MyBudgetApp.Enums;
using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBudgetApp.Validators.Dashboard
{
    public static class TransactionValidator
    {
        public static TransactionValidationResult Validate(string transactionAmount,
                                                           string description,
                                                           DateTime date,
                                                           IEnumerable<CategoryViewModel> categories)
        {
            if (string.IsNullOrEmpty(transactionAmount))
                return TransactionValidationResult.AmountEmpty;
            if (!decimal.TryParse(transactionAmount, out var amount))
                return TransactionValidationResult.AmountNotANumber;
            if (amount < 0)
                return TransactionValidationResult.AmountNegative;
            if (amount == 0)
                return TransactionValidationResult.AmountZero;
            if (amount > 1000000)
                return TransactionValidationResult.AmountTooLarge;
            if (!string.IsNullOrEmpty(description) && description.Length < 3)
                return TransactionValidationResult.DescriptionTooShort;
            if (!string.IsNullOrEmpty(description) && description.Length > 30)
                return TransactionValidationResult.DescriptionTooLong;
            if (!IsDateValid(date))
                return TransactionValidationResult.DateInvalid;
            if (!categories.Any())
                return TransactionValidationResult.CategoryNotSelected;
            return TransactionValidationResult.Success;
        }

        public static bool IsDateValid(DateTime date)
        {
            var now = DateTime.Now;
            var minDate = new DateTime(now.Year, 1, 1);
            var maxDate = new DateTime(now.Year + 1, 12, 31);

            return date >= minDate && date <= maxDate;
        }

        public static bool IsDeletionAllowed(TransactionViewModel transaction, decimal currentBalance) =>
            transaction.TransactionType != TransactionType.Income || transaction.Amount <= currentBalance;
    }
}