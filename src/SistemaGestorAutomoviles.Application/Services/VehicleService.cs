using SistemaGestorAutomoviles.Application.DTOs;
using SistemaGestorAutomoviles.Application.Exceptions;
using SistemaGestorAutomoviles.Application.Interfaces.IRepositories;
using SistemaGestorAutomoviles.Application.Interfaces.IServices;
using SistemaGestorAutomoviles.Domain.Entities;

namespace SistemaGestorAutomoviles.Application.Services;

public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
{
    public async Task<List<VehicleDto>> GetAllAsync()
    {
        var vehicles = await vehicleRepository.GetAllAsync();
        return vehicles.Select(MapToDto).ToList();
    }

    public async Task<VehicleDto?> GetByIdAsync(int id)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(id);
        return vehicle is null ? null : MapToDto(vehicle);
    }

    public async Task<VehicleDto> CreateAsync(VehicleCreateDto createDto)
    {
        ValidateBusinessRules(createDto.Year, createDto.Price, createDto.Mileage);

        var vehicle = new Vehicle
        {
            Brand = createDto.Brand.Trim(),
            Model = createDto.Model.Trim(),
            Year = createDto.Year,
            Price = createDto.Price,
            Color = createDto.Color.Trim(),
            Type = createDto.Type.Trim(),
            Transmission = createDto.Transmission.Trim(),
            Mileage = createDto.Mileage,
            IsAvailable = createDto.IsAvailable,
            RegistrationDate = DateTime.UtcNow
        };

        var createdVehicle = await vehicleRepository.AddAsync(vehicle);
        return MapToDto(createdVehicle);
    }

    public async Task<bool> UpdateAsync(int id, VehicleUpdateDto updateDto)
    {
        ValidateBusinessRules(updateDto.Year, updateDto.Price, updateDto.Mileage);

        var existingVehicle = await vehicleRepository.GetByIdAsync(id);
        if (existingVehicle is null)
        {
            return false;
        }

        existingVehicle.Brand = updateDto.Brand.Trim();
        existingVehicle.Model = updateDto.Model.Trim();
        existingVehicle.Year = updateDto.Year;
        existingVehicle.Price = updateDto.Price;
        existingVehicle.Color = updateDto.Color.Trim();
        existingVehicle.Type = updateDto.Type.Trim();
        existingVehicle.Transmission = updateDto.Transmission.Trim();
        existingVehicle.Mileage = updateDto.Mileage;
        existingVehicle.IsAvailable = updateDto.IsAvailable;

        await vehicleRepository.UpdateAsync(existingVehicle);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingVehicle = await vehicleRepository.GetByIdAsync(id);
        if (existingVehicle is null)
        {
            return false;
        }

        await vehicleRepository.DeleteAsync(existingVehicle);
        return true;
    }

    private static void ValidateBusinessRules(int year, decimal price, int mileage)
    {
        var currentYear = DateTime.UtcNow.Year + 1;
        if (year < 1950 || year > currentYear)
        {
            throw new BusinessValidationException($"Year must be between 1950 and {currentYear}.");
        }

        if (price <= 0)
        {
            throw new BusinessValidationException("Price must be greater than zero.");
        }

        if (mileage < 0)
        {
            throw new BusinessValidationException("Mileage cannot be negative.");
        }
    }

    private static VehicleDto MapToDto(Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            Year = vehicle.Year,
            Price = vehicle.Price,
            Color = vehicle.Color,
            Type = vehicle.Type,
            Transmission = vehicle.Transmission,
            Mileage = vehicle.Mileage,
            IsAvailable = vehicle.IsAvailable,
            RegistrationDate = vehicle.RegistrationDate
        };
    }
}
