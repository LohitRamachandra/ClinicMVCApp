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
            var from = _config["EmailSettings:From"];
            var host = _config["EmailSettings:Host"];
            var port = int.Parse(_config["EmailSettings:Port"]);
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            var mail = new MailMessage(new MailAddress(from), new MailAddress(patient.Email))
            {
                Subject = "Appointment Confirmation",
                Body = $@"Dear {patient.FirstName},

Your appointment has been successfully booked.

📅 Date: {appointment.Date:yyyy-MM-dd}
🕘 Time: {appointment.StartTime:hh\\:mm} – {appointment.EndTime:hh\\:mm}
🩺 Type: {appointment.AppointmentType}
📍 Status: {appointment.Status}

Thank you,
Wellness Centre",
                IsBodyHtml = false
            };

            await client.SendMailAsync(mail);
        }
    }

}
