using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _welcomeText = "Witaj w aplikacji budżetowej!";
        public string WelcomeText
        {
            get => _welcomeText;
            set
            {
                if (_welcomeText != value)
                {
                    _welcomeText = value;
                    OnPropertyChanged(nameof(WelcomeText));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
