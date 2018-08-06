namespace DentalSystem.Models
{
    public class ManipulationAppointment
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public int ManipulationId { get; set; }
        public Manipulation Manipulation { get; set; }
    }
}
