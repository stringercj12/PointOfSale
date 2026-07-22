using PointOfSale.Application.DTOs.Request.Product;

namespace PointOfSale.Application.UseCases.Products.Commands.UpdateProductBasicInfo;

public interface IUpdateProductBasicInfoUseCase
{
    Task ExecuteAsync(Guid productId, UpdateProductBasicInfoRequest request);
}