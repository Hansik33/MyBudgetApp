using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        public int SelectedCategoryId
        {
            get => SelectedCategory?.Id ?? 0;
            set
            {
                if (SelectedCategory != null && SelectedCategory.Id != value)
                    SelectedCategory = Categories.FirstOrDefault(category => category.Id == value);

            }
        }

        public ObservableCollection<string> Months { get; }

        private string? _selectedMonth;
        public string? SelectedMonth
        {
            get => _selectedMonth;
            set => SetProperty(ref _selectedMonth, value, nameof(SelectedMonth));
        }
        public int SelectedMonthNumber
        {
            get => Months.IndexOf(SelectedMonth ?? string.Empty) + 1;
            set
            {
                if (value < 1 || value > 12) return;
                SelectedMonth = Months[value - 1];
            }
        }

        public ObservableCollection<int> Years { get; }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set => SetProperty(ref _selectedYear, value, nameof(SelectedYear));
        }
        public int SelectedYearNumber
        {
            get => SelectedYear;
            set
            {
                if (value < Years.Min() || value > Years.Max()) return;
                SelectedYear = value;
            }
        }

        private string _limitAmount = string.Empty;
        public string LimitAmount
        {
            get => _limitAmount;
            set => SetProperty(ref _limitAmount, value, nameof(LimitAmount));
        }
        public decimal LimitAmountDecimal
        {
            get
            {
                if (decimal.TryParse(LimitAmount, out var amount))
                    return amount;
                return 0m;
            }
            set => LimitAmount = value.ToString("0.00");
        }

        public AddBudgetDialogViewModel(IEnumerable<CategoryViewModel> categories)
        {
            Categories = new ObservableCollection<CategoryViewModel>(categories);

            Months =
            [
                "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec",
                "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
            ];

            Years = [];
            int currentYear = DateTime.Now.Year;
            Years.Add(currentYear);
            int nextYear = currentYear + 1;
            Years.Add(nextYear);

            SelectedCategory = Categories.FirstOrDefault();
            SelectedMonth = Months[DateTime.Now.Month - 1];
            SelectedYear = currentYear;
        }
    }
}