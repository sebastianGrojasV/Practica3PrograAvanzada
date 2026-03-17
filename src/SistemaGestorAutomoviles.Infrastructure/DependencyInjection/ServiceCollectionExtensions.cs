using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaGestorAutomoviles.Application.Interfaces.IRepositories;
using SistemaGestorAutomoviles.Application.Interfaces.IServices;
using SistemaGestorAutomoviles.Application.Services;
using SistemaGestorAutomoviles.Infrastructure.Data;
using SistemaGestorAutomoviles.Infrastructure.Repositories;

namespace SistemaGestorAutomoviles.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IVehicleService, VehicleService>();

        return services;
    }
}
