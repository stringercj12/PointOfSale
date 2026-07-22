using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointOfSale.Application.UseCases.Common;
using PointOfSale.Application.UseCases.Common.Interfaces;
using PointOfSale.Domain.Repositories;
using PointOfSale.Infra.DataAccess;
using PointOfSale.Infra.Persistence.Repositories;
using PointOfSale.Infra.Services.Storage;

namespace PointOfSale.Infra;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddServices(services);
        ConfigureStorageProvider(services, configuration);

        return services;
    }

    private static void ConfigureStorageProvider(IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration["Storage:Provider"];

        switch (provider?.ToUpperInvariant())
        {
            case "S3":
                services.AddAWSService<IAmazonS3>(configuration.GetAWSOptions("Storage:S3"));
                services.AddScoped<IStorageService>(sp =>
                {
                    var s3Client = sp.GetRequiredService<IAmazonS3>();
                    var bucketName = configuration["Storage:S3:BucketName"]!;
                    return new S3StorageService(s3Client, bucketName);
                });
                break;

            case "LOCAL":
            default:
                services.AddScoped<IStorageService>(_ =>
                {
                    var basePath = configuration["Storage:LocalStorage:BasePath"] ??
                                   Path.Combine(Directory.GetCurrentDirectory(), "Storage");
                    return new LocalStorageService(basePath);
                });
                break;
        }
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<PointOfSaleDbContext>(options =>
            options.UseSqlServer(connectionString!));
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<ICryptoService, CryptoService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}