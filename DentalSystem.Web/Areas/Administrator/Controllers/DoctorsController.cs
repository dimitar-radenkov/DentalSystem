namespace DentalSystem.Web.Areas.Administrator.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DentalSystem.Services.Contracts;
    using DentalSystem.Web.Areas.Administrator.Models.BindingModels;
    using Microsoft.AspNetCore.Mvc;

    public class DoctorsController : AdministatorController
    {
        private readonly IDoctorsService doctorsService;

        public DoctorsController(IDoctorsService doctorsService)
        {
            this.doctorsService = doctorsService;
        }

        public IActionResult Index()
        {
            return View(this.doctorsService.All());
        }

        [HttpGet]
        public FileStreamResult ViewImage(int id)
        {
            var doctor = this.doctorsService.GetById(id);
            if (doctor == null)
            {
                return null;
            }

            return new FileStreamResult(
                new MemoryStream(doctor.Image),
                doctor.ImageContentType);
            
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddDoctorBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(bindingModel);
            }

            byte[] image = null;
            using (var ms = new MemoryStream())
            {
                await bindingModel.Image.CopyToAsync(ms);
                image = ms.ToArray();
            }

            await this.doctorsService.AddAsync(
                bindingModel.Name,
                bindingModel.Address,
                bindingModel.Email,
                bindingModel.Phone,
                image,
                bindingModel.Image.ContentType);

            return this.RedirectToAction(nameof(Index));
        }
    }
}