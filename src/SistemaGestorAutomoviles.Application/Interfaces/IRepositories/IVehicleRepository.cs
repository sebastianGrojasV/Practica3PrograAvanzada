using SistemaGestorAutomoviles.Domain.Entities;

namespace SistemaGestorAutomoviles.Application.Interfaces.IRepositories;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetByIdAsync(int id);
    Task<Vehicle> AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(Vehicle vehicle);
    Task<bool> ExistsAsync(int id);
}
