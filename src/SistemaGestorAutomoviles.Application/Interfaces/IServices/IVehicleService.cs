using SistemaGestorAutomoviles.Application.DTOs;

namespace SistemaGestorAutomoviles.Application.Interfaces.IServices;

public interface IVehicleService
{
    Task<List<VehicleDto>> GetAllAsync();
    Task<VehicleDto?> GetByIdAsync(int id);
    Task<VehicleDto> CreateAsync(VehicleCreateDto createDto);
    Task<bool> UpdateAsync(int id, VehicleUpdateDto updateDto);
    Task<bool> DeleteAsync(int id);
}
