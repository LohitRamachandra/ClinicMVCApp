using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using ClinicMVCApp.Services;
using ClinicMVCApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepo.GetAllAsync();
            return appointments != null ?
                        View(appointments) :
                        Problem("Patients data  is null.");
           
        }
        [HttpGet]
        public async Task<IActionResult> Book()
        {
            var patients = await _patientRepo.GetAllAsync();

            ViewBag.Patients = patients.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.FirstName} {p.Surname} (ID: {p.IDNumber})"
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(AppointmentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var patients = await _patientRepo.GetAllAsync();
                ViewBag.Patients = patients.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.FirstName} {p.Surname} (ID: {p.IDNumber})"
                }).ToList();

                ModelState.AddModelError("", "Please fix the highlighted fields.");
                return View(vm);
            }

            var appointment = new Appointment
            {
                PatientId = vm.PatientId,
                AppointmentType = vm.AppointmentType,
                Date = vm.Date,
                StartTime = vm.StartTime,
                EndTime = vm.StartTime.Add(TimeSpan.FromMinutes(15)),
                Status = "Pending",
                DateInserted = DateTime.Now
            };

            await _appointmentRepo.AddAsync(appointment);

            /*Optional added for email service as well*/
            //var patient = await _patientRepo.GetByIdAsync(vm.PatientId);
            //if (patient != null)
            //    await _emailService.SendConfirmationEmail(patient, appointment);

            return RedirectToAction("Confirmation", new { appointmentId = appointment.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int appointmentId)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return RedirectToAction("Book");
            }

            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(appointmentId);
            if (appointment == null)
                return RedirectToAction("Book");

            appointment.Status = "Confirmed";
            appointment.DateModified = DateTime.Now;

            await _appointmentRepo.UpdateAsync(appointment);

            TempData["ConfirmationSuccess"] = "Appointment confirmed successfully!";
            return RedirectToAction("Confirmation", new { appointmentId });
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
            if (appointment == null) return NotFound();

            var vm = new AppointmentEditViewModel
            {
                Id = appointment.Id,
                AppointmentType = appointment.AppointmentType,
                Date = (DateTime)appointment.Date,
                StartTime = (TimeSpan)appointment.StartTime,
                Status = appointment.Status
            };

            return View(vm);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentEditViewModel appointmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(appointmentVM);
            }

            var appointment = await _appointmentRepo.GetByIdAsync(appointmentVM.Id);
            if (appointment == null) return NotFound();

            // Update only the fields that should be changed
            appointment.AppointmentType = appointmentVM.AppointmentType;
            appointment.Date = appointmentVM.Date;
            appointment.StartTime = appointmentVM.StartTime;
            appointment.EndTime = appointmentVM.StartTime.Add(TimeSpan.FromMinutes(15));
            appointment.Status = appointmentVM.Status;
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
