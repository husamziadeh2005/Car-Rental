using System.ComponentModel.DataAnnotations;

namespace CarRentalPH.Models
{
    public class VehicleCategory
    {

        [Key]
        public int VehicleCategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;


        public EnumVehicleType VehicleType { get; set; } = EnumVehicleType.Economy;

        [Range(1, 100)]
        [Display(Name = "Maximum Passengers")]
        public int MaxPassengers { get; set; }


        //navigation 
        public ICollection<Vehicle>? Vehicles { get; set; }
    }
    public enum EnumVehicleType
    {
        Economy, Suv, laxury, Van, Sport

    }
}

