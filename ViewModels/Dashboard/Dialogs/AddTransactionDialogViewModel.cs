using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddTransactionDialogViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryViewModel> Categories { get; }
        public ObservableCollection<EnumDisplay<TransactionType>> Types { get; }
        public ObservableCollection<EnumDisplay<PaymentMethod>> Methods { get; }

        public AddTransactionDialogViewModel(IEnumerable<CategoryViewModel> categories)
        {
            Categories = new ObservableCollection<CategoryViewModel>(categories);

            Types =
            [
                new(TransactionType.Income, "Przychód"),
                new(TransactionType.Expense, "Wydatek")
            ];

            Methods =
            [
                new(PaymentMethod.Cash, "Gotówka"),
                new(PaymentMethod.Transfer, "Przelew"),
                new(PaymentMethod.Card, "Karta"),
                new(PaymentMethod.Mobile, "Płatność mobilna"),
                new(PaymentMethod.Other, "Inna")
            ];

            SelectedType = Types.FirstOrDefault();
            SelectedCategory = Categories.FirstOrDefault();
            SelectedMethod = Methods.FirstOrDefault();
            SelectedDate = DateTimeOffset.Now;
        }

        private EnumDisplay<TransactionType>? _selectedType;
        public EnumDisplay<TransactionType>? SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }
        public TransactionType SelectedTypeAsEnum
        {
            get => SelectedType?.Value ?? TransactionType.Expense;
            set => SelectedType = Types.FirstOrDefault(type => type.Value == value);
        }

        private CategoryViewModel? _selectedCategory;
        public CategoryViewModel? SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }
        public int SelectedCategoryId
        {
            get => SelectedCategory?.Id ?? 0;
            set
            {
                if (SelectedCategory != null && SelectedCategory.Id != value)
                    SelectedCategory = Categories.FirstOrDefault(category => category.Id == value);
            }
        }

        private string _amount = string.Empty;
        public string Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }
        public decimal AmountAsDecimal
        {
            get
            {
                if (decimal.TryParse(Amount, out var amount))
                    return amount;
                return 0m;
            }
            set => Amount = value.ToString("0.00");
        }

        private EnumDisplay<PaymentMethod>? _selectedMethod;
        public EnumDisplay<PaymentMethod>? SelectedMethod
        {
            get => _selectedMethod;
            set => SetProperty(ref _selectedMethod, value);
        }
        public PaymentMethod SelectedMethodAsEnum
        {
            get => SelectedMethod?.Value ?? PaymentMethod.Other;
            set => SelectedMethod = Methods.FirstOrDefault(method => method.Value == value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, StringFormatter.Format(value));
        }

        private DateTimeOffset _selectedDate = DateTimeOffset.Now;
        public DateTimeOffset SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
        public DateTime SelectedDateAsDateTime => SelectedDate.DateTime;
    }
}