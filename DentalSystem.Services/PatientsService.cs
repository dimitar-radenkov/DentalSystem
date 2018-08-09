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

        public async Task<IdentityResult> Add(string name, string email, string password, string phoneNumber) =>
            await this.userManager.CreateAsync(
                new User
                {
                    Name = name,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phoneNumber,
                },
                password);

        public IEnumerable<PatientViewModel> All() => 
            this.db.Users
                .Where(u => u.Type == UserType.Ordinary)
                .Select(u => this.mapper.Map<User, PatientViewModel>(u))
                .ToList();


    }
}
