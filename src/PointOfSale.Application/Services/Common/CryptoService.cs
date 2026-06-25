using PointOfSale.Application.Services.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Application.Services.Common
{
    public class CryptoService : ICryptoService
    {
        private const int WorkFactor = 12;

        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("A senha não pode ser nula ou vazia para gerar o hash.");

            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash))
                return false;

            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }
    }
}
