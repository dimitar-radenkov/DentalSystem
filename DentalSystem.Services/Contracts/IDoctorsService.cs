namespace DentalSystem.Services.Contracts
{
    using System.Collections.Generic;
    using DentalSystem.Models.ViewModels;

    public interface IDoctorsService
    {
        IEnumerable<DoctorViewModel> All();

        string Add(string name, string email, string phone, byte[] imageData, string imageContentType);

        DoctorViewModel GetById(string id);
        
    }
}
