namespace DentalSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Appointment
    {
        public Appointment()
        {
            this.Manipulations = new HashSet<ManipulationAppointment>();
        }

        public int Id { get; set; }

        public AppointmentStatus Status { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string DoctorId { get; set; }
        public User Doctor { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<ManipulationAppointment> Manipulations { get; set; }
    }
}
