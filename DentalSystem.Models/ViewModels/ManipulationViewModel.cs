namespace DentalSystem.Models.ViewModels
{
    using System;

    public class ManipulationViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal Price { get; set; }
    }
}
