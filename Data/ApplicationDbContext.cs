using ClinicMVCApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClinicMVCApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }


}
