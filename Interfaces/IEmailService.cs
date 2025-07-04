using ClinicMVCApp.Models;

namespace ClinicMVCApp.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationEmail(Patient patient, Appointment appointment);

    }
}
