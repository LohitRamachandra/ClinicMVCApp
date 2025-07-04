using ClinicMVCApp.Data;
using ClinicMVCApp.Interfaces;
using ClinicMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicMVCApp.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                 .Include(a => a.Patient)
                 .ToListAsync();
        }
        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _context.Appointments
                 .Include(a => a.Patient)
                 .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment); 
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }

}
