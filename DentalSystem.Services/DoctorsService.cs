namespace DentalSystem.Services
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using DentalSystem.Common.Contants;
    using DentalSystem.Common.Utils;
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
        private readonly UserManager<User> userManager;

        public DoctorsService(
            DentalSystemDbContext db,
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.db = db;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public string Add(
            string name,
            string email, 
            string phone, 
            byte[] imageData,
            string imageContentType)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                PhoneNumber = phone,
                EmailConfirmed = true,
            };

            var tempPassword = PasswordGenerator.Generate();
            Debug.WriteLine($"Random Password Generated : {tempPassword}"); 
            this.userManager.CreateAsync(user, tempPassword).Wait();
            this.userManager.AddToRoleAsync(user, Roles.DOCTOR).Wait();

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
            this.db.SaveChanges();

            return tempPassword;
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
