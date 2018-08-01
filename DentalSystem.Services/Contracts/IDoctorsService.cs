namespace DentalSystem.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DentalSystem.Models.ViewModels;

    public interface IDoctorsService
    {
        IEnumerable<DoctorViewModel> All();

        Task<int> AddAsync(string name, string address, string email, string phone, byte[] imageData, string imageContentType);

        DoctorViewModel GetById(int id);
        
    }
}
