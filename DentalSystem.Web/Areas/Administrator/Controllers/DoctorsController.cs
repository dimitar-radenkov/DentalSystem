namespace DentalSystem.Web.Areas.Administrator.Controllers
{
    using System;
    using System.IO;
    using DentalSystem.Services.Contracts;
    using DentalSystem.Web.Areas.Administrator.Models.BindingModels;
    using DentalSystem.Web.Extensions;
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
        public IActionResult Add(AddDoctorBindingModel bindingModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(bindingModel);
            }

            try
            {
                byte[] image = null;
                using (var ms = new MemoryStream())
                {
                    bindingModel.Image.CopyToAsync(ms).Wait();
                    image = ms.ToArray();
                }

                var generatedPassword = this.doctorsService.Add(
                    bindingModel.Name,
                    bindingModel.Email,
                    bindingModel.Phone,
                    image,
                    bindingModel.Image.ContentType);
            }
            catch (Exception)
            {
                this.AddDangerMessage("Some error occured");
                return this.View();
            }

            this.AddSuccessMessage("Doctor has been added successfully");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}