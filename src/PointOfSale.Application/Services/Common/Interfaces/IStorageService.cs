using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Application.Services.Common.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName);
    }
}
