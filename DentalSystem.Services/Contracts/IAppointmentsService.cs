namespace DentalSystem.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DentalSystem.Models;
    using DentalSystem.Models.ViewModels;

    public interface IAppointmentsService
    {
        IEnumerable<AppointmentViewModel> All();

        Task<Appointment> AddAsync(
            string patientId, 
            string doctorId, 
            DateTime dateTime,
            IEnumerable<int> manipulations);
    }
}
