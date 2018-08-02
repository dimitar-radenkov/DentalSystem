namespace DentalSystem.Web.Areas.Administrator.Controllers
{
    using System.IO;
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

        public IActionResult Index() => 
            this.View(this.doctorsService.All());

        [HttpGet]
        public IActionResult Add() => this.View();

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
                bindingModel.Email,
                bindingModel.Phone,
                image,
                bindingModel.Image.ContentType);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}