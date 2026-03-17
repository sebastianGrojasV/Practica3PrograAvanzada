using Microsoft.EntityFrameworkCore;
using SistemaGestorAutomoviles.Application.Interfaces.IRepositories;
using SistemaGestorAutomoviles.Domain.Entities;
using SistemaGestorAutomoviles.Infrastructure.Data;

namespace SistemaGestorAutomoviles.Infrastructure.Repositories;

public class VehicleRepository(AppDbContext dbContext) : IVehicleRepository
{
    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await dbContext.Vehicles.OrderByDescending(v => v.RegistrationDate).ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Vehicle> AddAsync(Vehicle vehicle)
    {
        dbContext.Vehicles.Add(vehicle);
        await dbContext.SaveChangesAsync();
        return vehicle;
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        dbContext.Vehicles.Update(vehicle);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Vehicle vehicle)
    {
        dbContext.Vehicles.Remove(vehicle);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await dbContext.Vehicles.AnyAsync(v => v.Id == id);
    }
}
