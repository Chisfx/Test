using Test.Application.Interfaces.Contexts;
using Test.Application.Interfaces.Repositories;
using Test.Application.Interfaces.Shared;
using Microsoft.Extensions.DependencyInjection;
using Test.Infrastructure.DbContexts;
using Test.Infrastructure.Repositories;
using Test.Infrastructure.Shared;

namespace Test.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICompareObject, CompareObjectService>();
        }
    }
}
