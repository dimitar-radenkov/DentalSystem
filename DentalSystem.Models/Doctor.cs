namespace DentalSystem.Models
{
    using Microsoft.AspNetCore.Identity;

    public class Doctor
    {
        public int Id { get; set; }

        public string FileId { get; set; }
        public File File { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
