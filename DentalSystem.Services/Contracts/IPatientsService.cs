namespace DentalSystem.Services.Contracts
{
    using System.Collections.Generic;
    using DentalSystem.Models.ViewModels;

    public interface IPatientsService
    {
        IEnumerable<PatientViewModel> All();
    }
}
