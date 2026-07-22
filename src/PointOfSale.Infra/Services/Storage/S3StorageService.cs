using Amazon.S3;
using Amazon.S3.Model;
using PointOfSale.Application.UseCases.Common.Interfaces;

namespace PointOfSale.Infra.Services.Storage;

public class S3StorageService : IStorageService
{
    private readonly string _bucketName;
    private readonly IAmazonS3 _s3Client;

    public S3StorageService(IAmazonS3 s3Client, string bucketName)
    {
        _s3Client = s3Client;
        _bucketName = bucketName;
    }

    public async Task<string> UploadAsync(Stream fileStream, string path, string contentType)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = path,
            InputStream = fileStream,
            ContentType = contentType
        };

        await _s3Client.PutObjectAsync(putRequest);
        return path;
    }

    public async Task DeleteAsync(string path)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = path
        };

        await _s3Client.DeleteObjectAsync(deleteRequest);
    }

    public async Task DeleteManyAsync(IEnumerable<string> paths)
    {
        var deleteRequest = new DeleteObjectsRequest
        {
            BucketName = _bucketName,
            Objects = paths.Select(p => new KeyVersion { Key = p }).ToList()
        };

        await _s3Client.DeleteObjectsAsync(deleteRequest);
    }
}