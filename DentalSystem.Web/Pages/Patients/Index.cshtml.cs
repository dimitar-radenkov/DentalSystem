namespace DentalSystem.Web.Pages.Patients
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DentalSystem.Common.Contants;
    using DentalSystem.Models.ViewModels;
    using DentalSystem.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize(Roles = Roles.ADMINISTRATOR + ", " + Roles.OFFICE_MANAGER)]
    public class IndexModel : PageModel
    {
        private readonly IPatientsService patientsService;

        public IEnumerable<PatientViewModel> PatientsAll { get; set; }

        public IndexModel(IPatientsService patientsService)
        {
            this.patientsService = patientsService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            this.PatientsAll = await this.patientsService.AllAsync();

            return this.Page();
        }
    }
}