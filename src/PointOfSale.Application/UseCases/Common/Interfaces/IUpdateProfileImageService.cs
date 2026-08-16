using Microsoft.AspNetCore.Http;

namespace PointOfSale.Application.UseCases.Common.Interfaces;

public interface IUpdateProfileImageService
{
    Task ExecuteAsync(Guid userId, IFormFile imageFile);
}