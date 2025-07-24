using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddBudgetDialogViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryViewModel> Categories { get; }

        private CategoryViewModel? _selectedCategory;
        public CategoryViewModel? SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value, nameof(SelectedCategory));
        }

        public ObservableCollection<string> Months { get; }

        private string? _selectedMonth;
        public string? SelectedMonth
        {
            get => _selectedMonth;
            set => SetProperty(ref _selectedMonth, value, nameof(SelectedMonth));
        }

        public ObservableCollection<int> Years { get; }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set => SetProperty(ref _selectedYear, value, nameof(SelectedYear));
        }

        private string _limitAmount = string.Empty;
        public string LimitAmount
        {
            get => _limitAmount;
            set => SetProperty(ref _limitAmount, value, nameof(LimitAmount));
        }

        public AddBudgetDialogViewModel(IEnumerable<CategoryViewModel> categories)
        {
            Categories = new ObservableCollection<CategoryViewModel>(categories);

            Months = new ObservableCollection<string>
            {
                "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec",
                "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
            };

            Years = new ObservableCollection<int>();
            int currentYear = DateTime.Now.Year;
            for (int y = currentYear - 2; y <= currentYear + 5; y++)
                Years.Add(y);

            SelectedMonth = Months[DateTime.Now.Month - 1];
            SelectedYear = currentYear;
        }
    }
}