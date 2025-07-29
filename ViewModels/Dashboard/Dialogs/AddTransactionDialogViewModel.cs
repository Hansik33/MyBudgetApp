using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddTransactionDialogViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryViewModel> Categories { get; }
        public ObservableCollection<string> Types { get; }
        public ObservableCollection<string> PaymentMethods { get; }

        private string? _selectedTransactionType;
        public string? SelectedTransactionType
        {
            get => _selectedTransactionType;
            set => SetProperty(ref _selectedTransactionType, value);
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

        private string? _selectedPaymentMethod;
        public string? SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set => SetProperty(ref _selectedPaymentMethod, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public AddTransactionDialogViewModel(IEnumerable<CategoryViewModel> categories)
        {
            Categories = new ObservableCollection<CategoryViewModel>(categories);
            Types = new ObservableCollection<string> { "Wydatek", "Przychód" };
            PaymentMethods = new ObservableCollection<string> { "Gotówka", "Przelew", "Karta" };

            SelectedTransactionType = Types.FirstOrDefault();
            SelectedCategory = Categories.FirstOrDefault();
            SelectedPaymentMethod = PaymentMethods.FirstOrDefault();
            SelectedDate = DateTime.Today;
        }
    }
}