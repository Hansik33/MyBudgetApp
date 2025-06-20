using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Data;
using MyBudgetApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class DatabaseService
    {
        private const string ConnectionString =
            "server=localhost;port=3306;database=mybudgetapp;user=root;password=qwertyz1234!";

        public bool TryConnect()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var db = new AppDbContext(options);
            bool connected = db.Database.CanConnect();

            Debug.WriteLine($"Połączenie z bazą: {connected}");
            return connected;
        }

        public bool InsertUser(string username, string passwordHash)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var db = new AppDbContext(options);

            bool userExists = db.Users.Any(u => u.Username == username);
            if (userExists)
                return false;

            db.Users.Add(new User
            {
                Username = username,
                Password_hash = passwordHash
            });

            db.SaveChanges();
            return true;
        }
    }
}
