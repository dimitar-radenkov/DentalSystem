namespace DentalSystem.Web.Pages.Patients
{
    using System.Linq;
    using System.Threading.Tasks;
    using DentalSystem.Common.Contants;
    using DentalSystem.Models.BindingModels;
    using DentalSystem.Services.Contracts;
    using DentalSystem.Web.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize(Roles = Roles.OFFICE_MANAGER + ", " + Roles.ADMINISTRATOR )]
    public class AddModel : PageModel
    {
        private readonly IPatientsService patientsService;

        [BindProperty]
        public AddPatientBindingModel InputModel { get; set; }

        public AddModel(IPatientsService patientsService)
        {
            this.patientsService = patientsService;
        }

        public IActionResult OnGet()
        {
            return this.Page();
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

           var result = await this.patientsService.AddAsync(
                this.InputModel.Name,
                this.InputModel.Email,
                this.InputModel.Password,
                this.InputModel.PhoneNumber);

            if (result.Errors.Any())
            {
                this.AddDangerMessage($"Unable to add patient: {string.Join(", ", result.Errors)}");
                return this.Page();
            }

            this.AddSuccessMessage($"User '{this.InputModel.Name}' has been added successfully");
            return this.RedirectToPage("/Index");
        }
    }
}