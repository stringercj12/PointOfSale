using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PointOfSale.Application.Services.Common;
using PointOfSale.Application.Services.Common.Interfaces;
using PointOfSale.Application.Services.Users.Create;
using PointOfSale.Application.Services.Users.Delete;
using PointOfSale.Application.Services.Users.Get;
using PointOfSale.Application.Services.Users.Login;
using PointOfSale.Application.Services.Users.Update;
using PointOfSale.Application.Validations;

namespace PointOfSale.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserCreateRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UserUpdateRequestValidator>();

            services.AddSingleton<ICryptoService, CryptoService>();

            services.AddScoped<IUserCreateService, UserCreateService>();
            services.AddScoped<IUserReadService, UserReadService>();
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<IUserUpdateService, UserUpdateService>();
            services.AddScoped<IUserChangePasswordService, UserChangePasswordService>(); 
            services.AddScoped<IUserDeleteService, UserDeleteService>(); 
            services.AddScoped<IStorageService, StorageService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IPaymentService, PaymentService>();
            //services.AddScoped<IOrderService, OrderService>();


        }
    }
}
