namespace DentalSystem.Web.Pages.Patients
{
    using System;
    using System.Threading.Tasks;
    using DentalSystem.Common.Contants;
    using DentalSystem.Models.ViewModels;
    using DentalSystem.Services.Contracts;
    using DentalSystem.Web.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize(Roles = Roles.ADMINISTRATOR + ", " + Roles.OFFICE_MANAGER)]
    public class DetailsModel : PageModel
    {
        private readonly IPatientsService patientsService;

        public PatientDetailsViewModel PatientModel;

        public DetailsModel(IPatientsService patientsService)
        {
            this.patientsService = patientsService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                this.PatientModel = await this.patientsService.GetDetailsAsync(id.ToString());
            }
            catch (Exception)
            {
                this.AddDangerMessage("Unable to get patient details");
            }

            return this.Page();
        }
    }
}