using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using MyBudgetApp.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace MyBudgetApp
{
    public partial class App : Application
    {
        private Window? _window;

        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            string connectionString = "server=localhost;port=3306;database=mybudgetapp;user=root;password=qwertyz1234!";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;

            using var db = new AppDbContext(options);

            bool connected = db.Database.CanConnect();
            Debug.WriteLine($"Połączenie z bazą: {connected}");

            _window = new MainWindow();
            _window.Activate();
        }
    } 
}
