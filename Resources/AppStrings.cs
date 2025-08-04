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
                public const string UserExists = "Taki użytkownik już istnieje!";
                public const string UserNotFound = "Nieprawidłowa nazwa użytkownika lub hasło!";

                public const string PasswordEmpty = "Wprowadź hasło!";
                public const string PasswordMismatch = "Hasła nie są identyczne!";

                public const string RegisterSuccess = "Rejestracja zakończona pomyślnie.";
                public const string LoginSuccess = "Zalogowano jako: {0}.";
                public const string UserLoggedOut = "Wylogowano pomyślnie.";
            }

            public static class Budget
            {
                public const string LimitEmpty = "Limit budżetu nie może być pusty!";
                public const string LimitNotANumber = "Limit budżetu musi być liczbą!";
                public const string LimitNegative = "Limit budżetu nie może być ujemny!";
                public const string LimitZero = "Limit budżetu musi być dodatni!";
                public const string LimitTooTooLarge = "Limit budżetu nie może przekraczać 1 miliona!";

                public const string CategoryNotSelected = "Wybierz kategorię dla budżetu!";

                public const string CreatedSuccess = "Budżet został utworzony.";
                public const string ConfirmDelete = "Czy na pewno chcesz usunąć ten budżet?";
                public const string DeletedSuccess = "Budżet został usunięty.";
            }

            public static class Category
            {
                public const string NameEmpty = "Nazwa kategorii nie może być pusta!";
                public const string NameTooShort = "Nazwa kategorii musi mieć co najmniej 2 znaki!";
                public const string NameTooLong = "Nazwa kategorii nie może przekraczać 30 znaków!";
                public const string NameExists = "Kategoria o tej nazwie już istnieje!";

                public const string CreatedSuccess = "Kategoria została utworzona.";
                public const string ConfirmDelete = "Czy na pewno chcesz usunąć tę kategorię?";
                public const string DeletionNotAllowed = "Kategoria jest używana i nie może zostać usunięta!";
                public const string DeletedSuccess = "Kategoria została usunięta.";
            }

            public static class Transaction
            {
                public const string AmountEmpty = "Kwota transakcji nie może być pusta!";
                public const string AmountNotANumber = "Kwota transakcji musi być liczbą!";
                public const string AmountNegative = "Kwota transakcji nie może być ujemna!";
                public const string AmountZero = "Kwota transakcji musi być dodatnia!";
                public const string AmountTooLarge = "Kwota transakcji nie może przekraczać 1 miliona!";

                public const string DescriptionTooShort = "Opis transakcji musi mieć co najmniej 3 znaki!";
                public const string DescriptionTooLong = "Opis transakcji nie może przekraczać 30 znaków!";

                public const string DateInvalid = "Data transakcji musi być w bieżącym lub przyszłym roku!";

                public const string CategoryNotSelected = "Wybierz kategorię dla transakcji!";

                public const string CreatedSuccess = "Transakcja została utworzona.";
                public const string ConfirmDelete = "Czy na pewno chcesz usunąć tę transakcję?";
                public const string DeletedSuccess = "Transakcja została usunięta.";
                public const string DeletionNotAllowed = "Nie można usunąć tej transakcji, ponieważ spowodowałoby to ujemne saldo!";
            }

            public static class Saving
            {
                public const string ConfirmDelete = "Czy na pewno chcesz usunąć tę wpłatę do celu?";
                public const string DeletedSuccess = "Wpłata do celu została usunięta.";
            }

            public static class SavingGoal
            {
                public const string NameEmpty = "Nazwa celu oszczędnościowego nie może być pusta!";
                public const string NameTooShort = "Nazwa celu oszczędnościowego musi mieć co najmniej 3 znaki!";
                public const string NameTooLong = "Nazwa celu oszczędnościowego nie może przekraczać 30 znaków!";
                public const string NameNotUnique = "Cel oszczędnościowy o tej nazwie już istnieje!";

                public const string TargetAmountEmpty = "Kwota docelowa nie może być pusta!";
                public const string TargetAmountNotANumber = "Kwota docelowa musi być liczbą!";
                public const string TargetAmountNegative = "Kwota docelowa nie może być ujemna!";
                public const string TargetAmountZero = "Kwota docelowa musi być dodatnia!";
                public const string TargetAmountTooLarge = "Kwota docelowa nie może przekraczać 1 miliona!";

                public const string DeadlineInvalid = "Termin celu oszczędnościowego musi być co najmniej miesiąc w przyszłość!";

                public const string CreatedSuccess = "Cel oszczędnościowy został utworzony.";
                public const string ConfirmDelete = "Czy na pewno chcesz usunąć ten cel oszczędnościowy?";
                public const string DeletedSuccess = "Cel oszczędnościowy został usunięty.";
            }
        }
    }
}