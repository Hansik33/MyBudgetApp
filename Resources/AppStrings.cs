namespace MyBudgetApp.Resources
{
    public static class AppStrings
    {
        public static class Dialogs
        {
            public const string TitleWarning = "⚠️ Ostrzeżenie";
            public const string TitleSuccess = "✅ Sukces";
            public const string TitleError = "❌ Błąd";
            public const string TitleInfo = "ℹ️ Informacja";

            public static class Auth
            {
                public const string UserEmpty = "Wprowadź nazwę użytkownika!";
                public const string PasswordEmpty = "Wprowadź hasło!";
                public const string PasswordMismatch = "Hasła nie są identyczne!";
                public const string UserExists = "Taki użytkownik już istnieje!";
                public const string UserNotFound = "Nieprawidłowa nazwa użytkownika lub hasło!";
                public const string RegisterSuccess = "Rejestracja zakończona pomyślnie.";
                public const string LoginSuccess = "Zalogowano jako: {0}";
                public const string UserLoggedOut = "Wylogowano pomyślnie.";
            }

            public static class Budget
            {
                public const string ConfirmDelete = "Czy na pewno chcesz usunąć ten budżet?";
                public const string DeletedSuccess = "Budżet został usunięty.";
            }

            public static class Transaction
            {
                public const string ConfirmDelete = "Czy na pewno chcesz usunąć tę transakcję?";
                public const string DeletedSuccess = "Transakcja została usunięta.";
            }

            public static class Saving
            {
                public const string ConfirmDelete = "Czy na pewno chcesz usunąć tę wpłatę do celu?";
                public const string DeletedSuccess = "Wpłata do celu została usunięta.";
            }
        }
    }
}