using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using ClinicMVCApp.ViewModels;
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

            var vm = new PatientEditViewModel
            {
                PatientID = patient.Id,
                FirstName = patient.FirstName,
                Surname = patient.Surname,
                IDNumber = patient.IDNumber,
                Email = patient.Email,
                //Phone = patient.Phone,
                //DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PatientEditViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var patient = await _patientRepo.GetByIdAsync(vm.PatientID);
            if (patient == null) return NotFound();

            // Update values
            patient.FirstName = vm.FirstName;
            patient.Surname = vm.Surname;
            patient.IDNumber = vm.IDNumber;
            patient.Email = vm.Email;
            //patient.Phone = vm.Phone;
            //patient.DateOfBirth = vm.DateOfBirth;
            patient.Gender = vm.Gender;
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


