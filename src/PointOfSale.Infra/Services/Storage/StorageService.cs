using PointOfSale.Application.UseCases.Common.Interfaces;

namespace PointOfSale.Infra.Services.Storage;

public class StorageService : IStorageService
{
    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        var caminhoDaImagem = Path.Combine("images", fileName);

        if (!Directory.Exists("images"))
            Directory.CreateDirectory("images");


        File.Create(caminhoDaImagem);

        return caminhoDaImagem;
    }

    public Task DeleteAsync(string path)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(IEnumerable<string> paths)
    {
        throw new NotImplementedException();
    }
}