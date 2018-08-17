namespace DentalSystem.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DentalSystem.Models.ViewModels;
    using Microsoft.AspNetCore.Identity;

    public interface IPatientsService
    {
        Task<IEnumerable<PatientViewModel>> AllAsync();

        Task<IdentityResult> AddAsync(string name, string email, string password, string phoneNumber);

        Task<PatientDetailsViewModel> GetDetailsAsync(string id);
    }
}
