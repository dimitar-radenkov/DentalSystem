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

    public class DoctorsService : IDoctorsService
    {
        private readonly DentalSystemDbContext db;
        private readonly IMapper mapper;

        public DoctorsService(DentalSystemDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;     
        }

        public Task<int> AddAsync(string name, string address, string email, string phone, byte[] imageData, string imageContentType)
        {
            var doctor = new Doctor
            {
                Name = name,
                Address = address,
                Email = email,
                Phone = phone,
                Image = imageData,
                ImageContentType = imageContentType
            };

            this.db.Doctors.Add(doctor);
            return this.db.SaveChangesAsync();
        }

        public IEnumerable<DoctorViewModel> All() =>
            this.db.Doctors
                .Select(this.mapper.Map<Doctor, DoctorViewModel>)
                .ToList();

        public DoctorViewModel GetById(int id) =>
            this.mapper.Map<Doctor, DoctorViewModel>(this.db.Doctors.Find(id));
    }
}
