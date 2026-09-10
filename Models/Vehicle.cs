using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalPH.Models
{
    public class Vehicle
    {

        [Key]
        public int VehicleId { get; set; }

        // fogin key 
        [ForeignKey("VehicleCategory")]
        public int VehicleCategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Range(2000, 2030)]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; } = string.Empty;

        [Range(1, 10000)]
        [Display(Name = "Daily Rental Rate")]
        [Column("Daily_Rental_Rate")]
        public decimal DailyRate { get; set; }

        public EnumVehicleStatus VehicleType { get; set; }


        //Navigation 
        public VehicleCategory? Category { get; set; }

        public ICollection<Rental>? Rentals { get; set; }

    }
    public enum EnumVehicleStatus
    {
        Available,
        Rented,
        Maintenance
    }
}

