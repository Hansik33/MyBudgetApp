using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Services;
using MyBudgetApp.ViewModels;
using MyBudgetApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace MyBudgetApp
{
    public sealed partial class MainWindow : Window
    {
        private readonly INavigationService _nav;

        public MainWindow()
        {
            InitializeComponent();

            _nav = new NavigationService(MainContent);
            _nav.GoToLogin();
        }
    }
}
