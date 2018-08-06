namespace DentalSystem.Web.Pages.Appointments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DentalSystem.Models.BindingModels;
    using DentalSystem.Services.Contracts;
    using DentalSystem.Web.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AddModel : PageModel
    {
        private readonly IDoctorsService doctorsService;
        private readonly IPatientsService patientsService;
        private readonly IAppointmentsService appointmentsService;
        private readonly IManipulationsService manipulationsService;

        [BindProperty]
        public AddAppointmentBindingModel InputModel { get; set; }

        public AddModel(
            IDoctorsService doctorsService,
            IPatientsService patientsService,
            IAppointmentsService appointmentsService,
            IManipulationsService  manipulationsService)
        {
            this.doctorsService = doctorsService;
            this.patientsService = patientsService;
            this.appointmentsService = appointmentsService;
            this.manipulationsService = manipulationsService;

            this.InitializeInputModel();
        }

        public void OnGet()
        {

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                this.InitializeInputModel();
                return this.Page();     
            }

            await this.appointmentsService.AddAsync(
                this.InputModel.PatientId, 
                this.InputModel.DoctorId,
                this.InputModel.DateTime,
                this.InputModel.SelectedManipulations);

            this.AddSuccessMessage("Appointment has been added successfully");

            return this.RedirectToPage("/Index");
        }


        private void InitializeInputModel()
        {
            this.InputModel = new AddAppointmentBindingModel()
            {
                DateTime = DateTime.Now,
                Patients = this.patientsService
                .All()
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id
                }),

                        Doctors = this.doctorsService
                .All()
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id
                }),

                        Manipulations = this.manipulationsService
                .All()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
            };
        }
    }
}