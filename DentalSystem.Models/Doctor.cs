namespace DentalSystem.Models
{
    using Microsoft.AspNetCore.Identity;

    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }


        public string FileId { get; set; }
        public File File { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
