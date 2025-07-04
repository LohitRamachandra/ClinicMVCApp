using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using System.Net;
using System.Net.Mail;

namespace ClinicMVCApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendConfirmationEmail(Patient patient, Appointment appointment)
        {
            var start = appointment.StartTime.Value.ToString(@"hh\:mm");
            var end = appointment.EndTime.Value.ToString(@"hh\:mm");


            var fromAddress = new MailAddress(_config["EmailSettings:From"], _config["EmailSettings:DisplayName"]);
            var toAddress = new MailAddress(patient.Email);
            var smtpHost = _config["EmailSettings:Host"];
            var smtpPort = int.Parse(_config["EmailSettings:Port"]);
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];

            var subject = "Appointment Confirmation";
            

            var body = $@"
Dear {patient.FirstName},

Your appointment has been successfully booked.

📅 Date: {appointment.Date:yyyy-MM-dd}
🕘 Time: {start} – {end}
🩺 Type: {appointment.AppointmentType}
📍 Status: {appointment.Status}

If you have questions, please contact the clinic.

Kind regards,  
dtic Clinic Centre Team";



            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(username, password)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            await client.SendMailAsync(message);
        }


    }

}
