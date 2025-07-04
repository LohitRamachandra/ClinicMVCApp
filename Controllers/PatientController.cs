using ClinicMVCApp.Interfaces;
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
            var patients = await _patientRepo.GetAllAsync();
            return patients != null ?
                        View(patients) :
                        Problem("Patients data  is null.");
        }

        // Add Create/Edit/Delete actions as needed
    }


}
