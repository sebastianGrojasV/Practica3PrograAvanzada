using Microsoft.AspNetCore.Mvc;
using SistemaGestorAutomoviles.Application.DTOs;
using SistemaGestorAutomoviles.Application.Interfaces.IServices;

namespace SistemaGestorAutomoviles.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController(IVehicleService vehicleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<VehicleDto>>> GetAll()
    {
        var vehicles = await vehicleService.GetAllAsync();
        return Ok(vehicles);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VehicleDto>> GetById(int id)
    {
        var vehicle = await vehicleService.GetByIdAsync(id);
        if (vehicle is null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult<VehicleDto>> Create([FromBody] VehicleCreateDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var created = await vehicleService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] VehicleUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var updated = await vehicleService.UpdateAsync(id, updateDto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await vehicleService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
