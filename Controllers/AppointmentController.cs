using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVCApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentRepository _appointmentRepo;


        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentRepository appointmentRepo)
        {
            _logger = logger;
            _appointmentRepo = appointmentRepo;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var appointments = await _appointmentRepo.GetAllAsync();
            return appointments != null ?
                        View(appointments) :
                        Problem("Patients data  is null.");
           
        }

        public async Task<IActionResult> Book()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRepo.AddAsync(appointment);
                return RedirectToAction("Confirmation");
            }
            return View(appointment);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
