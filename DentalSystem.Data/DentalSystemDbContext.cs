namespace DentalSystem.Data
{
    using DentalSystem.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DentalSystemDbContext : IdentityDbContext<User>
    {
        public DbSet<File> Files { get; set; }

        public DbSet<Manipulation> Manipulations { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<ManipulationAppointment> ManipulationsAppointments { get; set; }

        public DentalSystemDbContext(DbContextOptions<DentalSystemDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ManipulationAppointment>(e => 
            {
                e.HasKey(x => new { x.AppointmentId, x.ManipulationId });

                e.HasOne(x => x.Manipulation);
                e.HasOne(x => x.Appointment);
            });

            base.OnModelCreating(builder);  
        }
    }
}
