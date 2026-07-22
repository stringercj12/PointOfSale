namespace PointOfSale.Application.UseCases.Common.Interfaces;

public interface IStorageService
{
    Task<string> UploadAsync(Stream fileStream, string path, string contentType);

    Task DeleteAsync(string path);

    Task DeleteManyAsync(IEnumerable<string> paths);
}