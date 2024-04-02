using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace FlutterAPI.DTO.User
{
    public class UserRes
    {
        public string? NumberID { get; set; }
        public string? AccountID { get; set; }
        public string? FullName { get; set; }
        public DateTime DateCreated { get; set; }
        public string PhoneNumber { get; set; }
        public bool Locked { get; set; }

        public UserRes(FlutterAPI.Model.User user)
        {
            NumberID = user.NumberID;
            AccountID = user.Id;
            FullName = user.FullName!;
            PhoneNumber = user.PhoneNumber!;
            Locked = user.LockoutEnabled;
            DateCreated = DateTime.Now;
        }
    }
    public class UserAdminRes
    {
        public string? NumberID { get; set; }
        public string? AccountID { get; set; }
        public string? FullName { get; set; }
        public DateTime DateCreated { get; set; }
        public string PhoneNumber { get; set; }
        public bool Locked { get; set; }
        public string Password { get; set; }

        public UserAdminRes(FlutterAPI.Model.User user)
        {
            NumberID = user.NumberID;
            AccountID = user.Id;
            FullName = user.FullName!;
            PhoneNumber = user.PhoneNumber!;
            Locked = user.LockoutEnabled;
            DateCreated = DateTime.Now;
        }
    }
}
