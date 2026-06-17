using Microsoft.Extensions.DependencyInjection;
using PointOfSale.Application.Users.Create;

namespace PointOfSale.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IUserCreateService, UserCreateService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IPaymentService, PaymentService>();
            //services.AddScoped<IOrderService, OrderService>();
        }
    }
}
