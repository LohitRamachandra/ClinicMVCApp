using System.ComponentModel.DataAnnotations;

namespace ClinicMVCApp.ViewModels
{
    public class PatientEditViewModel
    {
        public int PatientID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string IDNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //public string Phone { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }
    }


}
