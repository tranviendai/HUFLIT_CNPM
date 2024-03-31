using Microsoft.AspNetCore.Identity;

namespace FlutterAPI.Model
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime? DateCreated { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
