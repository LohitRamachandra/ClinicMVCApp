using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClinicMVCApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientRepository _patientRepo;
        public PatientController(ILogger<PatientController> logger, IPatientRepository patientRepo)
        {
            _logger = logger;
            _patientRepo = patientRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _patientRepo.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpGet]
        public IActionResult Create() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (!ModelState.IsValid)
            { 
                return View(patient); 
            }

            patient.DateInserted = DateTime.Now;
            await _patientRepo.AddAsync(patient);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient patient)
        {
            if (!ModelState.IsValid) return View(patient);

            patient.DateModified = DateTime.Now;
            await _patientRepo.UpdateAsync(patient);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientRepo.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _patientRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}


