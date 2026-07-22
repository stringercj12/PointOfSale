using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PointOfSale.Application.Services.Users.Get;
using PointOfSale.Application.Services.Users.Update;
using PointOfSale.Application.UseCases.Categories.Commands.CreateCategory;
using PointOfSale.Application.UseCases.Categories.Queries.GetCategoriesList;
using PointOfSale.Application.UseCases.Common.Interfaces;
using PointOfSale.Application.UseCases.Products.Commands.CreateProduct;
using PointOfSale.Application.UseCases.Products.Commands.UpdateProductBasicInfo;
using PointOfSale.Application.UseCases.Products.Commands.UpdateProductCategories;
using PointOfSale.Application.UseCases.Products.Commands.UpdateProductPhotos;
using PointOfSale.Application.UseCases.Products.Queries.GetProductById;
using PointOfSale.Application.UseCases.Products.Queries.GetProductsList;
using PointOfSale.Application.UseCases.Users.Create;
using PointOfSale.Application.UseCases.Users.Delete;
using PointOfSale.Application.UseCases.Users.Login;
using PointOfSale.Application.UseCases.Users.Queries;
using PointOfSale.Application.UseCases.Users.Update;
using PointOfSale.Application.Validations.Users;

namespace PointOfSale.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //  FluentValidation
        services.AddValidatorsFromAssemblyContaining<UserCreateRequestValidator>();

        // Use Cases - Users
        services.AddScoped<IUserCreateService, UserCreateService>();
        services.AddScoped<IUserReadService, UserReadService>();
        services.AddScoped<IUserLoginService, UserLoginService>();
        services.AddScoped<IUserUpdateService, UserUpdateService>();
        services.AddScoped<IUserChangePasswordService, UserChangePasswordService>();
        services.AddScoped<IUserDeleteService, UserDeleteService>();
        services.AddScoped<IUpdateProfileImageService, UpdateProfileImageService>();

        //  Use Cases - Products
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IGetProductsListUseCase, GetProductsListUseCase>();
        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IUpdateProductBasicInfoUseCase, UpdateProductBasicInfoUseCase>();
        services.AddScoped<IUpdateProductPhotosUseCase, UpdateProductPhotosUseCase>();
        services.AddScoped<IUpdateProductCategoriesUseCase, UpdateProductCategoriesUseCase>();

        // Use Cases - Categories
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<IGetCategoriesListUseCase, GetCategoriesListUseCase>();

        return services;
    }
}