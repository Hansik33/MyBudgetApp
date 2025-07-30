using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
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

        private EnumDisplay<TransactionType>? _selectedType;
        public EnumDisplay<TransactionType>? SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }

        private CategoryViewModel? _selectedCategory;
        public CategoryViewModel? SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        private string _amount = string.Empty;
        public string Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        private EnumDisplay<PaymentMethod>? _selectedMethod;
        public EnumDisplay<PaymentMethod>? SelectedMethod
        {
            get => _selectedMethod;
            set => SetProperty(ref _selectedMethod, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private DateTimeOffset? _selectedDate = DateTimeOffset.Now;
        public DateTimeOffset? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

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
                new(PaymentMethod.Other, "Inne")
            ];

            SelectedType = Types.FirstOrDefault();
            SelectedCategory = Categories.FirstOrDefault();
            SelectedMethod = Methods.FirstOrDefault();
            SelectedDate = DateTimeOffset.Now;
        }
    }
}