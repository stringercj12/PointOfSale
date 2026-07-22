namespace PointOfSale.Application.DTOs.Request.Product;

public record ProductPhotoRequest(
    string FileName,
    string ContentType,
    Stream Stream,
    long Length
);