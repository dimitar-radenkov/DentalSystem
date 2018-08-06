namespace DentalSystem.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DentalSystem.Data;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;
    using Microsoft.EntityFrameworkCore;

    public class AppointmentsService : IAppointmentsService
    {
        private readonly DentalSystemDbContext db;
        private readonly IMapper mapper;

        public AppointmentsService(DentalSystemDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<Appointment> AddAsync(string patientId, string doctorId, DateTime dateTime, IEnumerable<int> manipulations)
        {
            var appointment = new Appointment
            {
                UserId = patientId,
                DoctorId = doctorId,
                DateTime = dateTime,                
                Status = AppointmentStatus.Scheduled
            };

            this.db.Appointments.Add(appointment);

            appointment.Manipulations = manipulations
                .Select(m => new ManipulationAppointment
                {
                    AppointmentId = appointment.Id,
                    ManipulationId = m
                })
                .ToList();

            this.db.ManipulationsAppointments.AddRange(appointment.Manipulations);
            await this.db.SaveChangesAsync();

            return appointment;
        }

        public IEnumerable<AppointmentViewModel> All() =>
            this.db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.User)
                .Include(a => a.Manipulations)
                .Select(a => this.mapper.Map<Appointment, AppointmentViewModel>(a))
                .ToList();
    }
}
