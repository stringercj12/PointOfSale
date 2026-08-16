namespace PointOfSale.Application.UseCases.Products.Commands.DeleteProduct
{
    public interface IDisableProductUseCase
    {
        Task ExecuteAsync(Guid productId);
    }
}
