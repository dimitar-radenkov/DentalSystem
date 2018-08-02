namespace DentalSystem.Data
{
    using DentalSystem.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DentalSystemDbContext : IdentityDbContext
    {
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<File> Files { get; set; }

        public DentalSystemDbContext(DbContextOptions<DentalSystemDbContext> options)
            : base(options)
        {

        }
    }
}
