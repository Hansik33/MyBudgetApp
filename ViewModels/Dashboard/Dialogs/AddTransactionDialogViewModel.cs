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
        public ObservableCollection<string> Methods { get; }

        private string _selectedType = string.Empty;
        public string SelectedType
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

        private string _selectedMethod = string.Empty;
        public string SelectedMethod
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

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public AddTransactionDialogViewModel(IEnumerable<CategoryViewModel> categories)
        {
            Categories = new ObservableCollection<CategoryViewModel>(categories);

            Types = new ObservableCollection<string> { "Przychód", "Wydatek" };
            Methods = new ObservableCollection<string> { "Gotówka", "Przelew", "Karta", "Płatność mobilna", "Inne" };

            SelectedType = Types.FirstOrDefault() ?? string.Empty;
            SelectedCategory = Categories.FirstOrDefault();
            SelectedMethod = Methods.FirstOrDefault() ?? string.Empty;
            SelectedDate = DateTime.Today;
        }
    }
}