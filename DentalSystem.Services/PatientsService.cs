namespace DentalSystem.Services
{
    using System;
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

    public class PatientsService : IPatientsService
    {
        private readonly DentalSystemDbContext db;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public PatientsService(
            DentalSystemDbContext db,
            IMapper mapper, 
            UserManager<User> userManager)
        {
            this.db = db;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddAsync(
            string name,
            string email, 
            string password,
            string phoneNumber) =>
            await this.userManager.CreateAsync(
                new User
                {
                    Name = name,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phoneNumber,
                },
                password);

        public async Task<IEnumerable<PatientViewModel>> AllAsync() => 
            await this.db.Users
                .Where(u => u.Type == UserType.Ordinary)
                .OrderBy(u => u.Name)
                .Select(u => this.mapper.Map<User, PatientViewModel>(u))
                .ToListAsync();

        public async Task<PatientDetailsViewModel> GetDetailsAsync(string id)
        {
            var patient = await this.db.Users
                .Where(u => u.Type == UserType.Ordinary)
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (patient == null)
            { 
                throw new ArgumentException(nameof(id));
            }

            var appointments = this.db.Appointments
                .Where(a => a.UserId == id)
                .Include(a => a.Doctor)
                .Include(a => a.Manipulations)
                    .ThenInclude(m => m.Manipulation)
                .Select(this.mapper.Map<Appointment, AppointmentViewModel>)
                .ToList();

            return 
                new PatientDetailsViewModel
                {
                    Name = patient.Name,
                    Appointments = appointments
                };

        }
    }
}
