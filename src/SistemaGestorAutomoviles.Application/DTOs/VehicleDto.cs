namespace SistemaGestorAutomoviles.Application.DTOs;

public class VehicleDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Transmission { get; set; } = string.Empty;
    public int Mileage { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime RegistrationDate { get; set; }
}
