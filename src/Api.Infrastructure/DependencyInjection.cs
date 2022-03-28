using Api.ApplicationCore.Interfaces;
using Api.Infrastructure.Persistence.Contexts;
using Api.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApiContext>(options =>
            options.UseSqlServer(defaultConnectionString));

            services.AddScoped<IBookRepository, BookRepository>();

            return services;
        }
    }
}