using System.ComponentModel.DataAnnotations;

namespace ClinicMVCApp.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string? Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ID Number must be exactly 13 digits.")]
        [StringLength(30)]
        public string? IDNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string? Gender { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "Number of children must be between 0 and 20.")]
        public int? NumberOfChildren { get; set; }

        [Required]
        public string? HomeAddress { get; set; }

        [StringLength(30)]
        public string? City { get; set; }

        [StringLength(30)]
        public string? Suburb { get; set; }

        [Required]
        [StringLength(30)]
        public string? Province { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Postal Code must be 5 digits.")]
        [StringLength(10)]
        public string? PostalCode { get; set; }

        public DateTime? DateInserted { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
    }



}
