using System.ComponentModel.DataAnnotations;

namespace ClinicMVCApp.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        [Required]
        [StringLength(50)]
        public string AppointmentType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime DateInserted { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
    }




}