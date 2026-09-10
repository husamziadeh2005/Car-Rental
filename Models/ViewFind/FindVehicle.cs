using CarRentalPH.Models;
namespace CarRentalPH.Models.ViewFind

{
    public class FindVehicle
    {
        public int? VehicleId { get; set; } 
        public String? Brand { get; set; } 
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
