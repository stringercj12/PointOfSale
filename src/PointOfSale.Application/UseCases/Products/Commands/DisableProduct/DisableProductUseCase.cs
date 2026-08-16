using PointOfSale.Application.Exceptions;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Products.Commands.DeleteProduct
{
    public class DisableProductUseCase : IDisableProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DisableProductUseCase(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid productId)
        {
            var product = await _productRepository.GetForUpdateAsync(productId)
                      ?? throw new NotFoundException($"Produto não encontrado.");

            product.ToggleActiveStatus();

            await _unitOfWork.SaveChangesAsync();

        }
    }
}
