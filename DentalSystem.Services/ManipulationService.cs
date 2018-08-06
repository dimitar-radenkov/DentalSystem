namespace DentalSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DentalSystem.Data;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;
    using DentalSystem.Services.Contracts;

    public class ManipulationService : IManipulationsService
    {
        private readonly DentalSystemDbContext db;
        private readonly IMapper mapper;

        public ManipulationService(DentalSystemDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<ManipulationViewModel> All() =>
            this.db.Manipulations
                .Select(m => this.mapper.Map<Manipulation, ManipulationViewModel>(m))
                .ToList();
    }
}
