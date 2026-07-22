namespace PointOfSale.Application.UseCases.Common.Interfaces;

public interface ICryptoService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}