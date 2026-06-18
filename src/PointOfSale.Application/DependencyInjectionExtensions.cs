using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PointOfSale.Application.Services.Users.Create;
using PointOfSale.Application.Services.Users.Get;
using PointOfSale.Application.Validations;

namespace PointOfSale.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserCreateRequestValidator>();
            services.AddScoped<IUserCreateService, UserCreateService>();
            services.AddScoped<IUserReadService, UserReadService>(); 
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IPaymentService, PaymentService>();
            //services.AddScoped<IOrderService, OrderService>();
        }
    }
}
