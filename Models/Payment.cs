using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalPH.Models
{
    public class Payment
    {

        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("Rental")]
        public int RentalId { get; set; }

        [Range(0.01, 100000)]
        [Display(Name = "Payment Amount")]
        [Column("Payment_Amount")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = "zell";

        [Required]
        [StringLength(20)]
        public string? PaymentStatus { get; set; }


        //navigation 

        public Rental? Rental { get; set; }

    }
}
