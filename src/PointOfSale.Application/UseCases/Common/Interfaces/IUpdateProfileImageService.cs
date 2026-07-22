using Microsoft.AspNetCore.Http;

namespace PointOfSale.Application.UseCases.Common.Interfaces;

public interface IUpdateProfileImageService
{
    Task ExecuteAsync(int userId, IFormFile imageFile);
}