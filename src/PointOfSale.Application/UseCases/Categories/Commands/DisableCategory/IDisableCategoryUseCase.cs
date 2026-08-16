namespace PointOfSale.Application.UseCases.Categories.Commands.DisableCategory
{
    public interface IDisableCategoryUseCase
    {
        Task ExecuteAsync(Guid categoryId);
    }
}
