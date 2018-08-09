namespace DentalSystem.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DentalSystem.Models.ViewModels;
    using Microsoft.AspNetCore.Identity;

    public interface IPatientsService
    {
        IEnumerable<PatientViewModel> All();

        Task<IdentityResult> Add(string name, string email, string password, string phoneNumber);
    }
}
