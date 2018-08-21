namespace DentalSystem.Models.BindingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AddAppointmentBindingModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Patient")]
        public string PatientId { get; set; }

        [Required]
        [Display(Name = "Doctor")]
        public string DoctorId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Select Manipulations")]
        public IList<int> SelectedManipulations { get; set; }


        public IEnumerable<SelectListItem> Manipulations { get; set; }

        public IEnumerable<SelectListItem> Patients { get; set; }

        public IEnumerable<SelectListItem> Doctors { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DateTime <= DateTime.Now)
            {
                yield return new ValidationResult("Date Time must be in the future");
            }
        }
    }
}
