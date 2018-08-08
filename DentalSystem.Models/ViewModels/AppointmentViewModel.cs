namespace DentalSystem.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public AppointmentStatus Status { get; set; }

        public string DoctorName { get; set; }

        public string PatientName { get; set; }

        public DateTime DateTime { get; set; }

        public IEnumerable<ManipulationViewModel> Manupulations { get; set; }

        public decimal TotalPrice => this.Manupulations.Sum(m => m.Price);
    }
}
