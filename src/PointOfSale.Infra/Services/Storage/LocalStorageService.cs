using PointOfSale.Application.UseCases.Common.Interfaces;

namespace PointOfSale.Infra.Services.Storage;

public class LocalStorageService : IStorageService
{
    private readonly string _baseStoragePath;

    public LocalStorageService(string baseStoragePath)
    {
        _baseStoragePath = string.IsNullOrWhiteSpace(baseStoragePath)
            ? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")
            : baseStoragePath;

        if (!Directory.Exists(_baseStoragePath)) Directory.CreateDirectory(_baseStoragePath);
    }

    public async Task<string> UploadAsync(Stream fileStream, string path, string contentType)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("O fluxo de dados (stream) é inválido ou está vazio.", nameof(fileStream));

        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("O caminho do arquivo não pode ser nulo ou vazio.", nameof(path));

        var normalizedPath = path.Replace('/', Path.DirectorySeparatorChar);
        var fullPath = Path.Combine(_baseStoragePath, normalizedPath);
        var directory = Path.GetDirectoryName(fullPath);

        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) Directory.CreateDirectory(directory);

        await using (var destinationStream = new FileStream(
                         fullPath,
                         FileMode.Create,
                         FileAccess.Write,
                         FileShare.None,
                         8192,
                         true))
        {
            if (fileStream.CanSeek) fileStream.Position = 0;

            await fileStream.CopyToAsync(destinationStream);
        }

        return path.Replace('\\', '/');
    }

    public Task DeleteAsync(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return Task.CompletedTask;

        var normalizedPath = path.Replace('/', Path.DirectorySeparatorChar);
        var fullPath = Path.Combine(_baseStoragePath, normalizedPath);

        if (File.Exists(fullPath)) File.Delete(fullPath);

        return Task.CompletedTask;
    }

    public async Task DeleteManyAsync(IEnumerable<string> paths)
    {
        if (paths is null) return;

        foreach (var path in paths) await DeleteAsync(path);
    }
}