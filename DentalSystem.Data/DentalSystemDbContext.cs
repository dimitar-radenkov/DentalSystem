namespace DentalSystem.Data
{
    using System;
    using System.Linq;
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


            this.Seed(builder);
           

            base.OnModelCreating(builder);  
        }


        private void Seed(ModelBuilder builder)
        {
            //add manipulatios
            builder.Entity<Manipulation>().HasData(
               new Manipulation { Id = 1, Name = "Extraction", Duration = TimeSpan.FromMinutes(30), Price = 50M },
               new Manipulation { Id = 2, Name = "Cleaning", Duration = TimeSpan.FromMinutes(40), Price = 80M },
               new Manipulation { Id = 3, Name = "Seal", Duration = TimeSpan.FromMinutes(40), Price = 150M },
               new Manipulation { Id = 4, Name = "Thoot Implant", Duration = TimeSpan.FromHours(2), Price = 600M });

            ////add Patients
            //var patients = Enumerable.Range(1, 100)
            //    .ToList()
            //    .Select((x) => new User
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name = Faker.Name.FullName(),
            //        PasswordHash = Faker.Lorem.Words(1).First(),
            //        Email = Faker.Internet.Email(),
            //        EmailConfirmed = true,
            //        Type = UserType.Ordinary
            //    });
            //builder.Entity<User>().HasData(patients);

        }
    }
}
