namespace DentalSystem.Models
{
    using System;

    public class File
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}
