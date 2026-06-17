using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PointOfSale.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Infra.Repositories.Interfaces;
using PointOfSale.Infra.Repositories;

namespace PointOfSale.Infra
{
    public static class DependencyInjectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();  
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IPaymentRepository, PaymentRepository>();
            //services.AddScoped<IOrderRepository, OrderRepository>();
        }   

        public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {

            var conectionString = configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<PointOfSaleDbContext>(config => config.UseMySQL(conectionString!));
        }
    }
}
