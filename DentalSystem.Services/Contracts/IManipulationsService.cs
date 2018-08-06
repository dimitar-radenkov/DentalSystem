namespace DentalSystem.Services.Contracts
{
    using System.Collections.Generic;
    using DentalSystem.Models.ViewModels;

    public interface IManipulationsService 
    {
        IEnumerable<ManipulationViewModel> All();
    }
}
