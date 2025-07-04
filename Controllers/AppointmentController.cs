using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using ClinicMVCApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVCApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly IEmailService _emailService;


        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentRepository appointmentRepo, IPatientRepository patientRepo, IEmailService emailService)
        {
            _logger = logger;
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
            _emailService = emailService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var appointments = await _appointmentRepo.GetAllAsync();
            return appointments != null ?
                        View(appointments) :
                        Problem("Patients data  is null.");
           
        }
        [HttpGet]
        public async Task<IActionResult> Book()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(Appointment appointment)
        {
            if (!ModelState.IsValid)
                return View(appointment);

            appointment.EndTime = appointment.StartTime.Value.Add(TimeSpan.FromMinutes(15));
            appointment.Status = "Pending";
            appointment.DateInserted = DateTime.Now;

            await _appointmentRepo.AddAsync(appointment);

            var patient = await _patientRepo.GetByIdAsync(appointment.Patient.Id);
            await _emailService.SendConfirmationEmail(patient, appointment);

            return RedirectToAction("Confirmation", new { appointmentId = appointment.Id });
        }


        public async Task<IActionResult> Confirmation(int appointmentId)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return RedirectToAction("Book");
            }

            return View(appointment);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _appointmentRepo.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return View(appointment);
            }

            appointment.DateModified = DateTime.Now;
            await _appointmentRepo.UpdateAsync(appointment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _appointmentRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }




    }
}
