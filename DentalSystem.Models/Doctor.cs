namespace DentalSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public byte[] Image { get; set; }
        public string ImageContentType { get; set; }
    }
}
