using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVCApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentRepository _repo;


        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
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
                await _repo.AddAsync(appointment);
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
