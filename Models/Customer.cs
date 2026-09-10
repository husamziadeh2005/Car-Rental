using System.ComponentModel.DataAnnotations;

namespace CarRentalPH.Models
{
    public class Customer
    {

        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Compare(nameof(Email))]
        public string EmailConfirm { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        [Required]
        [StringLength(20)]
        public string DriverLicenseNumber { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        //navigation
        public ICollection<Rental>? Rentals { get; set; }



    }
}
