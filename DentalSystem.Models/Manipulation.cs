namespace DentalSystem.Models
{
    using System;

    public class Manipulation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal Price { get; set; }
    }
}