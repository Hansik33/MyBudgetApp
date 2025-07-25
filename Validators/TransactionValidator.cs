﻿using MyBudgetApp.Enums;
using MyBudgetApp.ViewModels.Dashboard;

namespace MyBudgetApp.Validators
{
    public static class TransactionValidator
    {
        public static bool IsDeletionAllowed(TransactionViewModel transaction, decimal currentBalance) =>
            transaction.TypeEnum != TransactionType.Income || transaction.Amount <= currentBalance;
    }

}