namespace DentalSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DentalSystem.Data;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;
    using DentalSystem.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class DoctorsService : IDoctorsService
    {
        private readonly DentalSystemDbContext db;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public DoctorsService(
            DentalSystemDbContext db,
            IMapper mapper,
            UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public Task<int> AddAsync(
            string name,
            string email, 
            string phone, 
            byte[] imageData,
            string imageContentType)
        {
            var user = new IdentityUser
            {
                UserName = name,
                Email = email,
                PhoneNumber = phone,
                EmailConfirmed = true,
            };
            this.db.Users.Add(user);

            var file = new File
            {
                Data = imageData,
                ContentType = imageContentType,
            };
            this.db.Files.Add(file);

            var doctor = new Doctor
            {
                User = user,
                File = file,
            };
            this.db.Doctors.Add(doctor);

            return this.db.SaveChangesAsync();
        }

        public IEnumerable<DoctorViewModel> All() =>
            this.db.Doctors
                .Include(d => d.User)
                .Include(d => d.File)
                .Select(this.mapper.Map<Doctor, DoctorViewModel>)
                .ToList();

        public DoctorViewModel GetById(int id) =>
            this.mapper.Map<Doctor, DoctorViewModel>(
                this.db.Doctors
                    .Include(d => d.User)
                    .FirstOrDefault(d => d.Id == id));
    }
}
