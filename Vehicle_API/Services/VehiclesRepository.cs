using System.Collections.Generic;
using System.Linq;

public interface IVehiclesRepository
{
    IEnumerable<Vehicle> GetCarsByColor(string color);
    IEnumerable<Vehicle> GetBusesByColor(string color);
    IEnumerable<Vehicle> GetBoatsByColor(string color);
    bool GetCarHeadlightsStatus(int carId);
    void TurnOnOffHeadlights(int vehicleId);
    IEnumerable<Vehicle> DeleteCar(int id);
    public List<Vehicle> GetAllVehicles();
}

public class VehiclesRepository : IVehiclesRepository
{
    private readonly List<Vehicle> vehicles;

    public VehiclesRepository()
    {
        vehicles = new List<Vehicle>
        {
            new Car { Id = 1, Color = "Red", Wheels = 4, HeadlightsOn = false},
            new Bus { Id = 2, Color = "Blue"},
            new Boat { Id = 3, Color = "White"},
            new Car { Id = 4, Color = "Black", Wheels = 4, HeadlightsOn = true},
            new Bus { Id = 5, Color = "Red"},
            new Boat { Id = 6, Color = "Black"},
            new Car { Id = 7, Color = "Blue", Wheels = 4, HeadlightsOn = false},
            // Add more vehicles
        };
    }

    public List<Vehicle> GetAllVehicles()
    {
        return vehicles;
    }

    public IEnumerable<Vehicle> GetCarsByColor(string color)
    {
        return vehicles.OfType<Car>().Where(c => c.Color!.Equals(color, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Vehicle> GetBusesByColor(string color)
    {
        return vehicles.OfType<Bus>().Where(b => b.Color!.Equals(color, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Vehicle> GetBoatsByColor(string color)
    {
        return vehicles.OfType<Boat>().Where(b => b.Color!.Equals(color, StringComparison.OrdinalIgnoreCase));
    }

    public bool GetCarHeadlightsStatus(int carId)
    {
        var vehicle = vehicles.Find(v => v.Id == carId);

        if (vehicle is Car car)
        {
            return car.HeadlightsOn;
        }
        else
        {
            throw new InvalidOperationException($"Vehicle with ID {carId} is not a car.");
        }
    }

    public void TurnOnOffHeadlights(int carId)
    {
        var vehicle = vehicles.Find(v => v.Id == carId);

        if (vehicle is Car car)
        {
            car.HeadlightsOn = !car.HeadlightsOn;
        }
        else
        {
            throw new InvalidOperationException($"Vehicle with ID {carId} is not a car.");
        }
    }

    public IEnumerable<Vehicle> DeleteCar(int id)
    {
        var cardelete = vehicles.OfType<Car>().SingleOrDefault(c => c.Id == id);

        if (cardelete != null)
        {
            vehicles.Remove(cardelete);
        }
        else
        {
            throw new InvalidOperationException($"Vehicle with ID {id} is not a car.");
        }
        return vehicles;
    }

}