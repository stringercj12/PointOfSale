using Microsoft.AspNetCore.Http;

namespace PointOfSale.Application.Services.Common.Interfaces
{
    public interface IUpdateProfileImageService
    {
        Task ExecuteAsync(int userId, IFormFile imageFile);
    }
}
