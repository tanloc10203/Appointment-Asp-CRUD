using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Appointment.Models;

namespace Appointment.Data
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext (DbContextOptions<AppointmentContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment.Models.DoctorModel> DoctorModel { get; set; } = default!;
    }
}
