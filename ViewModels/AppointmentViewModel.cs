using System.ComponentModel.DataAnnotations;

namespace ClinicMVCApp.ViewModels
{
    public class AppointmentViewModel
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public string AppointmentType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }
    }


}
