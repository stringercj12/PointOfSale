using PointOfSale.Application.DTOs.Request.Product;

namespace PointOfSale.Application.UseCases.Products.Commands.UpdateProductPhotos;

public interface IUpdateProductPhotosUseCase
{
    Task ExecuteAsync(UpdateProductPhotosRequest request);
}