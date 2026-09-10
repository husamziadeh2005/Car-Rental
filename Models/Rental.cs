using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalPH.Models
{
    public class Rental
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Range(1, 100000)]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public EnumRentalStatus Status { get; set; }

        //navigation 
        public Customer? Customer { get; set; }

        public Vehicle? Vehicle { get; set; }

        public ICollection<Payment>? Payments { get; set; }

    }
    public enum EnumRentalStatus
    {
        Pending,
        Active,
        Completed,
        Cancelled
    }
}

