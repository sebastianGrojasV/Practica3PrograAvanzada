using System.ComponentModel.DataAnnotations;

namespace SistemaGestorAutomoviles.Application.DTOs;

public class VehicleCreateDto
{
    [Required]
    [MaxLength(60)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [MaxLength(60)]
    public string Model { get; set; } = string.Empty;

    [Range(1950, 2100)]
    public int Year { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [MaxLength(40)]
    public string Color { get; set; } = string.Empty;

    [Required]
    [MaxLength(40)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Transmission { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int Mileage { get; set; }

    public bool IsAvailable { get; set; } = true;
}
