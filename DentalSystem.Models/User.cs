namespace DentalSystem.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string FileId { get; set; }
        public File File { get; set; }

        public UserType Type { get; set; }
    }
}
