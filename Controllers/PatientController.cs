using System.Diagnostics;
using ClinicMVCApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVCApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientRepository _repo;
        public PatientController(ILogger<PatientController> logger, IPatientRepository repo) 
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await _repo.GetAllAsync();
            return View(patients);
        }

        // Add Create/Edit/Delete actions as needed
    }


}
