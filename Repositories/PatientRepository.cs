﻿using ClinicMVCApp.Data;
using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicMVCApp.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }
        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }
        public async Task AddAsync(Patient patient) 
        { 
            _context.Patients.Add(patient); 
            await _context.SaveChangesAsync(); 
        }
        public async Task UpdateAsync(Patient patient) 
        { 
            _context.Patients.Update(patient); 
            await _context.SaveChangesAsync(); 
        }
        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null) 
            {
                _context.Patients.Remove(patient); 
                await _context.SaveChangesAsync(); 
            }
        }
    }

}
