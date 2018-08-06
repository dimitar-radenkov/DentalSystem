namespace DentalSystem.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AppointmentsController : AdministatorController
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }
    }
}