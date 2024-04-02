using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlutterAPI.Model
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ImageURL { get; set; }
        public string? NumberID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Gender { get; set; }
        public string? SchoolYear { get; set; }
        public string? SchoolKey { get; set; }
        public bool? Active { get; set; }
    }
}
