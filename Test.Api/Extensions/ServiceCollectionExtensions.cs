using Microsoft.EntityFrameworkCore;
using Test.Api.Interfaces;
using Test.Api.Services;
using Test.Infrastructure.DbContexts;

namespace Test.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApiPresentations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDirectorio, DirectorioRestService>();
            services.AddScoped<IVentas, VentasRestService>();
        }

        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
        }
    }
}
