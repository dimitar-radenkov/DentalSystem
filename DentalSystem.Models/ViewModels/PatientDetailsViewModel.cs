namespace DentalSystem.Models.ViewModels
{
    using System.Collections.Generic;

    public class PatientDetailsViewModel
    {
        public string Name { get; set; }

        public IEnumerable<AppointmentViewModel> Appointments { get; set; }
    }
}
