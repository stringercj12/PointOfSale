using PointOfSale.Application.Services.Common.Interfaces;
using System.IO;

namespace PointOfSale.Application.Services.Common
{
    public class StorageService : IStorageService
    {
        public async Task<string> UploadAsync(Stream fileStream, string fileName)
        {
            string caminhoDaImagem = Path.Combine("images", fileName);

            if (!Directory.Exists("images"))
                Directory.CreateDirectory("images");



            File.Create(caminhoDaImagem);

            return caminhoDaImagem;
        }
    }
}
