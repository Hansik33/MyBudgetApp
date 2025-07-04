using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Data;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;
using System.Diagnostics;
using System.Linq;

namespace MyBudgetApp.Services
{
    public class DatabaseService : IDatabaseService
    {
        private const string ConnectionString =
            "server=localhost;port=3306;database=mybudgetapp;user=root;password=qwertyz1234!";
        private readonly IPasswordHashService _passwordHashService;

        public DatabaseService(IPasswordHashService passwordHashService)
        {
            _passwordHashService = passwordHashService;
        }

        public bool TryConnect()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);
            bool connected = appDbContext.Database.CanConnect();

            Debug.WriteLine($"Połączenie z bazą: {connected}");
            return connected;
        }

        public bool InsertUser(string username, string plainPassword)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            if (appDbContext.Users.Any(u => u.Username == username))
                return false;

            var hashed = _passwordHashService.Hash(plainPassword);

            appDbContext.Users.Add(new User
            {
                Username = username,
                Password_hash = hashed
            });

            appDbContext.SaveChanges();
            return true;
        }

        public bool AuthenticateUser(string username, string plainPassword)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var db = new AppDbContext(options);

            var user = db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return false;

            return _passwordHashService.Verify(plainPassword, user.Password_hash);
        }
    }
}
