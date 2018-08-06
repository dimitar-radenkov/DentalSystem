namespace DentalSystem.Web.Pages.Appointments
{
    using System.Collections.Generic;
    using DentalSystem.Models.ViewModels;
    using DentalSystem.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly IAppointmentsService appointmentsService;

        public IEnumerable<AppointmentViewModel> Appointments { get; set; }

        public IndexModel(IAppointmentsService appointmentsService)
        {
            this.appointmentsService = appointmentsService;
        }

        public IActionResult OnGet()
        {
            this.Appointments = this.appointmentsService.All();

            return this.Page();
        }
    }
}