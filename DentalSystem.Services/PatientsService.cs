namespace DentalSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DentalSystem.Data;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;
    using DentalSystem.Services.Contracts;

    public class PatientsService : IPatientsService
    {
        private readonly DentalSystemDbContext db;
        private readonly IMapper mapper;

        public PatientsService(DentalSystemDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<PatientViewModel> All() => 
            this.db.Users
                .Where(u => u.Type == UserType.Ordinary)
                .Select(u => this.mapper.Map<User, PatientViewModel>(u))
                .ToList();


    }
}
