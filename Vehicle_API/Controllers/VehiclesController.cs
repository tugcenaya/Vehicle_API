using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IVehiclesRepository _repository;

    public VehiclesController(IVehiclesRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("cars")]
    public IActionResult GetCarsByColor([FromQuery] string color)
    {
        if (string.IsNullOrEmpty(color))
        {
            return BadRequest("Color parameter is missing or invalid.");
        }

        var cars = _repository.GetCarsByColor(color);

        if (!cars.Any())
        {
            return NotFound($"No cars with color '{color}' were found.");
        }

        return Ok(cars);
    }

    [HttpGet("buses")]
    public IActionResult GetBusesByColor([FromQuery] string color)
    {
        if (string.IsNullOrEmpty(color))
        {
            return BadRequest("Color parameter is missing or invalid.");
        }

        var buses = _repository.GetBusesByColor(color);

        if (!buses.Any())
        {
            return NotFound($"No buses with color '{color}' were found.");
        }

        return Ok(buses);
    }

    [HttpGet("boats")]
    public IActionResult GetBoatsByColor([FromQuery] string color)
    {
        if (string.IsNullOrEmpty(color))
        {
            return BadRequest("Color parameter is missing or invalid.");
        }

        var boats = _repository.GetBoatsByColor(color);

        if (!boats.Any())
        {
            return NotFound($"No boats with color '{color}' were found.");
        }
        
        return Ok(boats);
    }

    [HttpPost("turnonoffheadlights/{id}")]
    public IActionResult TurnOnOffHeadlights(int id)
    {
        try
        {
            _repository.TurnOnOffHeadlights(id);
            bool headlightsOn = _repository.GetCarHeadlightsStatus(id);
            return Ok($"Headlights turned {(headlightsOn ? "on" : "off")}");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
      
    }

    [HttpDelete("deletecar/{id}")]
    public IActionResult DeleteCar(int id)
    {
        try
        {
            var vehicles = _repository.DeleteCar(id);
            return Ok(new { Message = "Car has been deleted!", Vehicles = vehicles });
        }
        catch (Exception ex)
        {
            var vehicles = _repository.GetAllVehicles();
            return BadRequest(new { Message = ex.Message, Vehicles = vehicles});

        }
      
    }
    
}