namespace DentalSystem.Web.Pages.Appointments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DentalSystem.Common.Contants;
    using DentalSystem.Models;
    using DentalSystem.Models.BindingModels;
    using DentalSystem.Services.Contracts;
    using DentalSystem.Web.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IDoctorsService doctorsService;
        private readonly IPatientsService patientsService;
        private readonly IAppointmentsService appointmentsService;
        private readonly IManipulationsService manipulationsService;
        private readonly UserManager<User> userManager;

        [BindProperty]
        public AddAppointmentBindingModel InputModel { get; set; }

        public AddModel(
            IDoctorsService doctorsService,
            IPatientsService patientsService,
            IAppointmentsService appointmentsService,
            IManipulationsService  manipulationsService,
            UserManager<User> userManager)
        {
            this.doctorsService = doctorsService;
            this.patientsService = patientsService;
            this.appointmentsService = appointmentsService;
            this.manipulationsService = manipulationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            this.InputModel = new AddAppointmentBindingModel()
            {
                DateTime = DateTime.Now,              
            };

            await this.PopulateDropDownLists();

            return this.Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                await this.PopulateDropDownLists();
                return this.Page();     
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (this.CurrentUserIsPatient() && 
                currentUser != null &&
                currentUser.Id != this.InputModel.PatientId)
            {
                await this.PopulateDropDownLists();
                this.AddDangerMessage("You are not allowed to make appointment for somebody else.");
                return this.Page();
            }

            var result = await this.appointmentsService.AddAsync(
                this.InputModel.PatientId, 
                this.InputModel.DoctorId,
                this.InputModel.DateTime,
                this.InputModel.SelectedManipulations);

            this.AddSuccessMessage("Appointment has been added successfully");

            return this.RedirectToPage("/Index");
        }


        private bool CurrentUserIsPatient() => 
            !this.User.IsInRole(Roles.ADMINISTRATOR) && 
            !this.User.IsInRole(Roles.OFFICE_MANAGER);

        private async Task PopulateDropDownLists()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            this.InputModel.Patients = await this.patientsService
                .AllAsync()
                .ContinueWith((task) =>
                    task.Result.Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id,
                        Disabled = this.CurrentUserIsPatient() &&
                                         currentUser?.Id != p.Id,
                        Selected = this.CurrentUserIsPatient() &&
                                         currentUser?.Id == p.Id
                    })
                    .ToList());         


            this.InputModel.Doctors = this.doctorsService
                .All()
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id
                })
                .ToList();

            this.InputModel.Manipulations = this.manipulationsService
                .All()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
                .ToList();
        }
    }
}