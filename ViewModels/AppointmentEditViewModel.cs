using System.ComponentModel.DataAnnotations;

namespace ClinicMVCApp.ViewModels
{
    public class AppointmentEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string AppointmentType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public string Status { get; set; }

    }
}
