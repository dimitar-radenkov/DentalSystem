namespace DentalSystem.Models.ViewModels
{
    public class DoctorViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public FileViewModel File { get; set; }
    }
}
