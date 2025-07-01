using MyBudgetApp.Interfaces;
using System;
using System.Security.Cryptography;

namespace MyBudgetApp.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000;

        public string Hash(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256).GetBytes(KeySize);

            return "$PBKDF2$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(key);
        }

        public bool Verify(string password, string hashedPassword)
        {
            try
            {
                var parts = hashedPassword.Split('$');
                if (parts.Length != 4 || parts[1] != "PBKDF2")
                    return false;

                var salt = Convert.FromBase64String(parts[2]);
                var hash = Convert.FromBase64String(parts[3]);

                var key = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256).GetBytes(KeySize);
                return CryptographicOperations.FixedTimeEquals(key, hash);
            }
            catch
            {
                return false;
            }
        }
    }
}
