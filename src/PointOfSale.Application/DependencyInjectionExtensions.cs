using Microsoft.Extensions.DependencyInjection;
using PointOfSale.Application.Users.Create;
using PointOfSale.Application.Validations;
using FluentValidation;

namespace PointOfSale.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserCreateRequestValidator>();
            services.AddScoped<IUserCreateService, UserCreateService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IPaymentService, PaymentService>();
            //services.AddScoped<IOrderService, OrderService>();
        }
    }
}
